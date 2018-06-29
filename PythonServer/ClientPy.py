#!/usr/bin/env python
import socket
import os
ss=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
ss.connect(('127.0.0.1',8123))
#f=open('aa','wb')
ss.sendall('wo shini yeye')
os.system('sleep 1')
ss.send('EOF')
data=ss.recv(1024)
print "server dafu %s"%data
ss.close()