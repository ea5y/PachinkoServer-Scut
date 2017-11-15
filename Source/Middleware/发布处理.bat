@echo off
xcopy /y ZyGames.Framework.Game\bin\ZyGames.Framework.Game.* ..\..\Release\Pk-Server\ /EXCLUDE:copyfilter.txt
xcopy /y ScutSecurity\bin\*.dll ..\..\Release\Pk-Server\ /EXCLUDE:copyfilter.txt

xcopy /y GameServer\bin\GameServer.* ..\..\Release\Pk-Server\ /EXCLUDE:copyfilter.txt
xcopy /y GameServer\bin\RNLog.config ..\..\Release\Pk-Server\
xcopy /y /s GameServer\Script\*.* ..\..\Release\Pk-Server\Script\

ECHO ·¢²¼Íê±Ï£¡& PAUSE