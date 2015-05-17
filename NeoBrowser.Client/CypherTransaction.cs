using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    public class CypherTransaction
    {

        public enum TransactionState
        {
            CreatedInClient,
            CreatedOnServer,
            StatementSent,
            RolledBack,
            Committed,
            Faulted
        }

        private RestConnection _connection;
        private Uri _transactionUrl;

        internal CypherTransaction()
        {

        }

        internal CypherTransaction(RestConnection connection, bool forceCreation)
        {
            State = TransactionState.CreatedInClient;
            _connection = connection;
            _transactionUrl = null;
            if (forceCreation)
            {
                // _transactionUrl = await _connection.StartTransaction();
                // _state = TransactionState.CreatedOnServer;
            }
        }

        public TransactionState State { get; private set; }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

    }
}
