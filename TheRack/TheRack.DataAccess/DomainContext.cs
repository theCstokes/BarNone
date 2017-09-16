using Npgsql;
using TheRack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class DomainContext : IDisposable
    {
        #region Local Connection Data.
        public static readonly string HOST = "127.0.0.1";
        public static readonly string PORT = "5432";
        public static readonly string USERNAME = "postgres";
        public static readonly string PASSWORD = "admin";
        public static readonly string DATABASE = "SharpSight";
        #endregion

        public static string Credentials
        {
            get
            {
                return String.Format("User ID={0};Password={1};Host={2};Port={3};Database={4};",
                    USERNAME, PASSWORD, HOST, PORT, DATABASE);
            }
        }

        #region Public Definition(s).
        public delegate T RequestAction<T>(List<Dictionary<string, object>> results);
        #endregion

        #region Private Field(s).
        //private SemaphoreSlim queryBlock;
        private List<string> errorResults;
        //private ErrorReporter errorReporter;
        #endregion

        #region Public Constructor(s).
        public DomainContext(DomainParameters domainParams)
        {
            Connection = new NpgsqlConnection(Credentials);
            Connection.Open();
            Transaction = Connection.BeginTransaction();

            AccountID = domainParams.AccountID;

            //queryBlock = new SemaphoreSlim(1, 1);

            this.errorResults = new List<string>();
            //this.errorReporter = errorReporter;
        }

        public DomainContext()
        {
            Connection = new NpgsqlConnection(Credentials);
            Connection.Open();
            Transaction = Connection.BeginTransaction();

            //queryBlock = new SemaphoreSlim(1, 1);

            this.errorResults = new List<string>();
        }
        #endregion

        #region Public Property(s).
        public int AccountID { get; private set; }
        #endregion

        #region Public Member(s).
        public NpgsqlTransaction Transaction { get; private set; }
        public NpgsqlConnection Connection { get; private set; }

        public void Commit()
        {
            Transaction.Commit();
        }

        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Connection.Dispose();
        }
        #endregion

        //#region IErrorReporter Implementation.
        //public void Fail(bool didPass)
        //{
        //    errorReporter.Fail(didPass);
        //}
        //#endregion
    }
}
