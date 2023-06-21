using BLL.Coil;
using Controller.Coil;
using Controller.MsgPro;
using Core.Define;
using MsgConvert;
using MsgConvert.DBTable;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;

namespace UnitTest.Logic
{
    public class CoilProLogicTest
    {
        private CoilProLogic coilPro = new CoilProLogic();

        private ICoilController _coilController = new CoilController();

        private IMsgProController _msgPro = new MsgProController();

        [Test]
        public void GetUnScheduleCoilIDs()
        {
            var coilIDs = coilPro.GetUnScheduleCoilIDs(40).ToList();
            Assert.IsTrue(coilIDs.Count > 0);
        }


        [Test]
        public void GetScheduleCoilIDByPlanNo()
        {
            var coilIDs = coilPro.QueryScheduleCoilIDByPlanNo("A123456733").ToList();
            Assert.IsTrue(coilIDs.Count > 0);
        }

        [Test]
        public void GetPDO()
        {
            var pdo = coilPro.GetPDO("CA200000010000");
            Assert.IsTrue(pdo != null);
        }

        [Test]
        public void GetCollScheduleIDs()
        {
            var coilIDs = coilPro.QueryCollScheduleIDs(40);
            Assert.IsTrue(coilIDs.Count > 0);
        }

        [Test]
        public void UpdateTotalGrindLength()
        {

            try
            {
                var updateNum = coilPro.UpdateBeltAccLengthByGrNo(1, 1050, 200);
                Assert.IsTrue(updateNum > 0);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        [Test]
        public void GetTheLastDefectData()
        {
            //var DefectData = coilPro.GetDefectData();
            //Assert.IsTrue(DefectData != null);

        }

        [Test]
        public void GetBeltPatterns()
        {
            var beltPatterns = coilPro.QueryBeltPatterns("HE20010001");
            Assert.IsTrue(beltPatterns != null);

        }


        [Test]
        public void GetBeltPass()
        {
            //var beltPatterns = coilPro.GetBeltPasses("CE20010055").ToList();

            var beltPatterns = coilPro.QueryBeltPatternsByBelt("111").ToList();
            //var d = beltPatterns.Where(x => x.Pass_Section.Equals("H")).Select(v => v.PassNumber).FirstOrDefault();
            Assert.IsTrue(beltPatterns != null);
        }


        [Test]
        public void GenSplitChildrenCoilNoAndSave(){

            // using Controller.Coil;            
            ICoilController _coilController = new CoilController();
            var parentCoilID = "CE20010001";
            var childCoilNo = _coilController.GenSplitChildrenCoilNo(parentCoilID);           
            var inserOK = _coilController.VaildNewChildCoilNoData(childCoilNo, parentCoilID);
            Assert.IsTrue(inserOK);

        }

        [Test]
        public void UpdatePDOFinishTIme()
        {
           
            _coilController.UpdatePDOFinishTime("CE20020000", DateTime.Now);
       
        }


        [Test]
        public void TestCreate()
        {

            try
            {
                var data = coilPro.GetGR6LastRecordsParticleNo("123");
            }
            catch(Exception e)
            {

            }
            
          

        }

    }
}
