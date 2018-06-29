#-*- coding:utf-8 -*-
import BaseHTTPServer
import os

class RequestHandler(BaseHTTPServer.BaseHTTPRequestHandler):
    '''处理请求并返回页面'''

    # 页面模板
    Page = "zheshiyigeceshi"
    #os.system("python testPy2.py")
    
    # 处理一个GET请求
    def do_GET(self):
    	buf = self.rfile
        self.send_response(200)
        self.send_header("Welcome", "Contect")
        self.end_headers()
        self.wfile.write(buf)

#----------------------------------------------------------------------

if __name__ == '__main__':
    serverAddress = ('', 8989)
    server = BaseHTTPServer.HTTPServer(serverAddress, RequestHandler)
    server.serve_forever()