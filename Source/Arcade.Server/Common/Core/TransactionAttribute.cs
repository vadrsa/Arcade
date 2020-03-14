using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace Common.Core
{

    [PSerializableAttribute]
    public class TransactionAttribute : MethodInterceptionAspect
    {
        #region Fields


        #endregion

        public TransactionAttribute() { }
        public TransactionAttribute(IsolationLevel level) { IsolationLevel = level; }

        #region Public Properties

        public IsolationLevel IsolationLevel { get; set; }

        #endregion

        #region Public Methods and Operators

        public async override Task OnInvokeAsync(MethodInterceptionArgs args)
        {
            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel }, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await args.ProceedAsync();
                scope.Complete();
            }
            finally
            {
                scope.Dispose();
            }

        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel });
            try
            {
                args.Proceed();
                scope.Complete();
            }
            finally
            {
                scope.Dispose();
            }
        }

        #endregion
    }
}
