using DBService.Base;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;

namespace DBService.Repository.ConnectionStatus
{
    public class ConnectionStatusRepo : BaseRepository<TBL_ConnectionStatus>
    {
        public ConnectionStatusRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(TBL_ConnectionStatus);
        protected override string[] PKName => new string[] { nameof(TBL_ConnectionStatus.Connection_From),
                                                             nameof(TBL_ConnectionStatus.Connection_To)};
    }
}
