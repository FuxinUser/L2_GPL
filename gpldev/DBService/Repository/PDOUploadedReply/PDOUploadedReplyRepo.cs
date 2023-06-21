using DBService.Base;
using DBService.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Author:ICSC 余士鵬
 * Date:2019/12/24
 * Desc:DB Table
 */

namespace DBService.Repository
{
    public class PDOUploadedReplyRepo : BaseRepository<PDOUploadedReplyEntity.TBL_PDOUploadedReply>
    {

        public PDOUploadedReplyRepo(string connStr) : base(connStr)
        {

        }

        protected override string TableName => nameof(PDOUploadedReplyEntity.TBL_PDOUploadedReply);

        protected override string[] PKName => throw new NotImplementedException();//new string[] { nameof(PDOUploadedReplyEntity.TBL_PDOUploadedReply.Out_Coil_ID) };



    }
}
