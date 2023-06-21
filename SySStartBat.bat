echo off
::後續指令改用 UTF-8 編碼
chcp 65001

::設置程式開啟延遲
set OpenTimeout=5
::目標路徑
set TargetPath=Target
::執行模式
set ExeMode=Debug
::通訊程式名稱(用空格隔開)
set ConnApp=BCScnMgr LabelPrint MMSComm PcComm PLCComm WMSComm
::邏輯程式名稱(用空格隔開)
set LogicApp=CoilManager DataGathering DataSetup Tracking
::支援程式名稱(用空格隔開)
set SupportApp=LogRecord 

(for %%a in (%ConnApp%) do (
  tasklist /fo csv|findstr /i %%a > nul || start "" "%TargetPath%\%%a\bin\%ExeMode%\%%a"
  timeout /t %OpenTimeout%
))
(for %%a in (%LogicApp%) do (
  tasklist /fo csv|findstr /i %%a > nul || start "" "%TargetPath%\%%a\bin\%ExeMode%\%%a"
  timeout /t %OpenTimeout%
))
(for %%a in (%SupportApp%) do (
  tasklist /fo csv|findstr /i %%a > nul || start "" "%TargetPath%\%%a\bin\%ExeMode%\%%a"
  timeout /t %OpenTimeout%
))

goto exit

:exit