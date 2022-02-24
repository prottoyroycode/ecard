using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.bKash;
using Models.Entities;
using Models.Securities;
using System.Data;
using System.Threading.Tasks;

namespace Models.DataContext
{
    public class EfDbContext : DbContext, IEfDbContext
    {
        #region Constructor

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        public EfDbContext() : base()
        {
        }

        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasKey(t => t.Id);
            builder.Entity<UserFcmDeviceHistory>().HasKey(t => t.Id);
            builder.Entity<UserLoginHistory>().HasKey(t => t.Id);
            builder.Entity<UserPasswordHistory>().HasKey(t => t.Id);
            builder.Entity<Order>().HasKey(t => t.Id);
            builder.Entity<OrderDetails>().HasKey(t => t.OrderDetailsId);
            builder.Entity<UserPasswordResetCode>().HasKey(t => t.Id);
            builder.Entity<CallbackData>().HasKey(t => t.Id);
            

        }

        #region DbSet

        public DbSet<User> User { get; set; }
        public DbSet<UserFcmDeviceHistory> UserFcmDeviceHistory { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<UserPasswordHistory> UserPasswordHistory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<EmailSMTPConfiguration> EmailSMTPConfigurations { get; set; }
        public DbSet<UserPasswordResetCode> UserPasswordResetCodes { get; set; }
        public DbSet<CallbackData> CallbackDatas { get; set; }


        public DbSet<OrderResponse> OrderResponses { get; set; }
        //public DbSet<OrderedProduct> OrderedProducts { get; set; }

        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<bKashCreateAgreementRequest> bKashCreateAgreementRequests { get; set; }
        public DbSet<bKashCreateAgreementResponse> bKashCreateAgreementResponses { get; set; }
        public DbSet<bKashCallback> bKashCallbacks { get; set; }
        public DbSet<RockVilleOrderData> RockVilleOrderDatas { get; set; }
        public DbSet<ECardVoucher> ECardVouchers { get; set; }
        public DbSet<bKashAgreementException> bKashAgreementExceptions { get; set; }
        public DbSet<bKashExecAgreementRequest> bKashExecAgreementRequests { get; set; }
        public DbSet<bKashExecAgreementResponse> bKashExecAgreementResponses { get; set; }
        public DbSet<bKashCancelAgreementRequest> bKashCancelAgreementRequests { get; set; }
        public DbSet<bKashCancelAgreementResponse> bKashCancelAgreementResponses { get; set; }




        #endregion

        #region Transactions

        private IDbContextTransaction _transaction;

        public void BeginTran()
        {
            _transaction = Database.BeginTransaction();
        }
        public async Task BeginTranAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public void CommitTran()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public async Task CommitTranAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public void RollbackTran()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        public async Task RollbackTranAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        #endregion

        public async Task<string> SqlQueryToGetJson(string sqlQuery)
        {
            var table = new DataTable();
            var conn = Database.GetDbConnection();
            if (conn.State.ToString() != "Open")
            {
                await conn.OpenAsync();
            }

            var command = conn.CreateCommand();
            command.CommandText = sqlQuery;

            using (var reader = await command.ExecuteReaderAsync())
            {
                table.Load(reader);
            }
            if (conn.State.ToString() == "Open")
            {
                await conn.CloseAsync();
            }
            return table.Rows[0][0].ToString();
        }
    }
}
