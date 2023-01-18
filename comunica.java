import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

public class comunica extends Thread {
Socket myS ;
DataInputStream in;
DataOutputStream out;
    comunica(Socket myS){
        this.myS=myS;    
        try {
            in = new DataInputStream(myS.getInputStream());
            out = new DataOutputStream(myS.getOutputStream());
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    @Override
    public void run(){
        
    }
    
}
