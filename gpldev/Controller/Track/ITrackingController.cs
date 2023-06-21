using Core.Define;
using DBService.Repository;
using LogSender;
using MsgStruct;
using static DBService.Repository.CoilMapEntity;
using static MsgStruct.L1L2Rcv;

namespace Controller.Track
{
    public interface ITrackingController
    {
        void SetLog(ILog log);
        bool IsSystemAutoValueOn(string parameterGroup, string parameter);

        bool InvaildHasEntryTopCoilID();

      
        void UpdateCoilMapPOSCoilID(string coilID, L2SystemDef.SKPOS POS);

        void DeleteCoilNoFromDB(string coilID);
        TBL_CoilMap GetTrackMap();

        bool UpdateTrackMap(Msg_105_Trk_Map msg);

       
        bool Create25CoilMap(Msg_105_Trk_Map msg);




    }
}
