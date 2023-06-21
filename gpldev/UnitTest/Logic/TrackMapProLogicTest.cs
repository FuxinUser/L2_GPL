using BLL.Trck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Repository.CoilMapEntity;

namespace UnitTest.Logic
{
    public class TrackMapProLogicTest
    {
        private string _conStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["DB_Name"]].ConnectionString;
        private TrackMapProLogic trkPro = new TrackMapProLogic();

        [Test]
        public void GetCoilMap()
        {
            var coilMap = trkPro.GetCoilMap();
            Assert.IsTrue(coilMap!=null);
        }

        [Test]
        public void UpdateTrkMap()
        {
            var trkMap = new TBL_CoilMap();
            var updateOK = trkPro.UpdateTrkMap(trkMap);
            Assert.IsTrue(updateOK);
        }
    }
}
