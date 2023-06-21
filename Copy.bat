echo off
::後續指令改用 UTF-8 編碼
chcp 65001

::來源路徑
set SourcePath=gpldev
::目標路徑
set TargetPath=Target
::要處理的資料夾名稱(用空格隔開)
set ProjectNames=BCScnMgr CoilManager GPL DataGathering DataSetup LabelPrint LogRecord MMSComm PcComm PlcComm Tracking WMSComm 
::要複製的資料夾名稱
set CopyFolder=bin

::echo %ProjectNames%

::迴圈次數依 ProjectNames 的項目數量而定
(for %%a in (%ProjectNames%) do (
  if exist "%TargetPath%\%%a" (
    call :DeleteFolder "%TargetPath%\%%a"
    call :CreateFolderStructure "%TargetPath%\%%a\%CopyFolder%"
    call :CopySourceFilesToTarget "%SourcePath%\%%a\%CopyFolder%", "%TargetPath%\%%a\%CopyFolder%"
  ) else (
    call :CreateFolderStructure "%TargetPath%\%%a\%CopyFolder%"
    call :CopySourceFilesToTarget "%SourcePath%\%%a\%CopyFolder%", "%TargetPath%\%%a\%CopyFolder%"
  )
))
COPY %SourcePath%\SystemConfig.ini %TargetPath%\SystemConfig.ini 

pause

goto exit

::刪除目標資料夾
::%1 為傳入的參數
::del /Q(不詢問直接刪) /S(範圍內的所有檔案及子目錄)
:DeleteFolder
  del %1 /Q /S
  exit /b

::建立目標資料夾
::%1 為傳入的參數
:CreateFolderStructure
  md %1
  exit /b

::將來源路徑的內容複製到目標路徑
::%1(Source), %2(Target) 為傳入的參數
::xcopy /E(範圍內的所有檔案及子目錄)
:CopySourceFilesToTarget
  xcopy %1 %2 /E
  exit /b
  
:exit