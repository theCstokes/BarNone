#!python

import subprocess
import os
import sys

user = "postgres"
pw = "admin"
host = "127.0.0.1"
port = sys.argv[1];
db="BarNone"


dir_name = os.getcwd()
dump = """ "C:\\Program Files\\PostgreSQL\\10\\bin\\pg_dump" -h %s -p %s -U %s -Z 9 -f "%s" -F c %s  """                  
os.putenv('PGPASSWORD', pw)
 
file_name = db + ".backup"
command = dump % (host, port, user,  dir_name + "\\" + file_name, db)
print(command)
subprocess.call(command,shell = True)
print("%s Dump finished" % db)

