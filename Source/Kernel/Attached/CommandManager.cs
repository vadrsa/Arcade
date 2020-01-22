using Kernel.Utility;
using Kernel.Workitems;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Kernel
{
    public class CommandManager
    {
        const string DefaultWorkitemID = "General";

        private static Dictionary<string, Dictionary<string, CompositeCommand>> commandMap = new Dictionary<string, Dictionary<string, CompositeCommand>>();

        public static string GetCommandName(DependencyObject obj)
        {
            return (string)obj.GetValue(CommandNameProperty);
        }
        public static void SetCommandName(DependencyObject obj, string value)
        {
            obj.SetValue(CommandNameProperty, value);
        }
        public static readonly DependencyProperty CommandNameProperty =
            DependencyProperty.RegisterAttached("CommandName", typeof(string), typeof(CommandManager), new PropertyMetadata(null, OnCommandNameChanged));

        public static void OnOwnerChanged(DependencyObject element)
        {

            if (!DesignerProperties.GetIsInDesignMode(element))
            {
                string name = GetCommandName(element);
                if (!String.IsNullOrEmpty(name))
                    CreateCommand(element, name);
            }
        }

        private static void OnCommandNameChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            if (!DesignerProperties.GetIsInDesignMode(element))
            {
                CreateCommand(element, args.NewValue as string);
            }
        }

        private static IWorkItem GetOwnerWorkitem(DependencyObject element)
        {
            DependencyObject temp = element;
            while (temp != null)
            {
                IWorkItem workItem = WorkitemManager.GetOwner(temp);
                if (workItem != null) return workItem;
                temp = LogicalTreeHelper.GetParent(temp);
            }
            return null;
        }

        private static void CreateCommand(DependencyObject element, string command)
        {

            var a = GetOwnerWorkitem(element);
            IWorkItem workItem = GetOwnerWorkitem(element);
            
            PropertyInfo cmdProp = element.GetType().GetProperty("Command");
            if (cmdProp != null && cmdProp.CanWrite)
            {
                cmdProp.SetValue(element, GetCompositeCommandByName(command, workItem));
            }
        }

        private static ICommand WrapCommandAddParameter(ICommand command, Func<object> getParam)
        {
            DelegateCommand wrapped = new DelegateCommand(() =>
            {
                command.Execute(getParam());
            },
            () => command.CanExecute(getParam()));
            command.CanExecuteChanged += (o, e) => wrapped.RaiseCanExecuteChanged();
            return wrapped;
        }

        public static IDisposable RegisterCommand(string commandName, ICommand command)
        {
            return RegisterWorkitemCommand(commandName, null, command);
        }

        public static void UnregisterCommand(string commandName, ICommand command)
        {
            UnregisterWorkitemCommand(commandName, null, command);
        }

        public static IDisposable RegisterCommand(string commandName, Action action)
        {
            return RegisterWorkitemCommand(commandName, null, action);
        }

        public static IDisposable RegisterWorkitemCommand(string commandName, IWorkItem workItem, ICommand command)
        {
            CompositeCommand compositeCommand = GetCompositeCommandByName(commandName, workItem);
            compositeCommand.RegisterCommand(command);
            return new DisposableAction(() => compositeCommand.UnregisterCommand(command));
        }

        public static void UnregisterWorkitemCommand(string commandName, IWorkItem workItem, ICommand command)
        {
            GetCompositeCommandByName(commandName, workItem).UnregisterCommand(command);
        }

        public static void ClearWorkitemCommands(IWorkItem workItem)
        {
            if (commandMap.ContainsKey(workItem.WorkItemID))
                commandMap.Remove(workItem.WorkItemID);
        }

        public static IDisposable RegisterWorkitemCommand(string commandName, IWorkItem workItem, Action action)
        {
            CompositeCommand compositeCommand = GetCompositeCommandByName(commandName, workItem);
            ICommand command = new DelegateCommand(action);
            compositeCommand.RegisterCommand(command);
            return new DisposableAction(() => compositeCommand.UnregisterCommand(command));
        }

        public static void ExecuteWorkitemCommand(string commandName, IWorkItem workItem, object parameter = null)
        {
            ICommand cmd = GetCompositeCommandByName(commandName, workItem);
            if (cmd.CanExecute(parameter))
                cmd.Execute(parameter);
        }

        public static void ExecuteCommand(string commandName, object parameter = null)
        {
            ExecuteWorkitemCommand(commandName, null, parameter);
        }

        public static ICommand GetApplicationCommand(string name)
        {
            return GetCompositeCommandByName(name);
        }

        private static CompositeCommand GetCompositeCommandByName(string commandName, IWorkItem workItem = null)
        {
            string workItemID = workItem?.WorkItemID ?? DefaultWorkitemID;
            if (!commandMap.ContainsKey(workItemID))
                commandMap.Add(workItemID, new Dictionary<string, CompositeCommand>());
            var workitemCommands = commandMap[workItemID];

            if (!workitemCommands.ContainsKey(commandName))
                workitemCommands.Add(commandName, new CompositeCommand());
            return workitemCommands[commandName];
        }
    }
}
