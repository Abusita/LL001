
@echo off
cd Proto
set client_dest_path="..\ProtobufCS"
for %%i in (*.*) do protoc --csharp_out=%client_dest_path% %%i
echo success
pause