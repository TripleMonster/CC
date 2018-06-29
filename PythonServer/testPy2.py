#!/usr/bin/python
import socket
host='127.0.0.1'
port=8123
s=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
s.bind((host,port))
s.listen(2)
try:
       while True:
               conn,add=s.accept()
               while True:
                       data2=''
                       data1=conn.recv(3)
                       if data1=='EOF':
                               conn.send('hello clietn1')
                               break
                       if data1=='FOE':
                               conn.send('hello client2')
                               break
                       data2+=data1
                       print data2
except KeyboardInterrupt:
       print "you have CTRL+C,Now quit"
       s.close()