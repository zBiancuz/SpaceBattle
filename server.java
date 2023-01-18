import java.io.*;
import java.net.*;
public class server{

    
            
    /**
     * @param args
     * @throws IOException
     */
    public static void main(String[] args) throws IOException {

        ServerSocket server = null;
        Socket socketClient = null;
        int porta=1027;
        comunica com ;
        server = new ServerSocket(porta);

        while(true){
                         
            socketClient = server.accept();
            com=new comunica(socketClient);
            com.start();
            
                        
        }
    }
    
}