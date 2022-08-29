package client;

import contrato.IContratoRMI;
import contrato.Reply;
import contrato.Request;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.ArrayList;
import java.util.List;

public class ClientRMI {

    static String serverIP="localhost";
    static int registerPort = 7000;

    public static void main(String[] args) {

        try {
            Registry registry = LocateRegistry.getRegistry(serverIP,registerPort);
            //System.out.println("vai fazer lookup");
            IContratoRMI svc=(IContratoRMI)registry.lookup("RemServer");
            List<String> lines = LerNlinhas();
            Request req=new Request();
            req.setTextLines(lines);

            long start = System.currentTimeMillis();
            Reply rpy=svc.doRemoteWork(req);
            long end = System.currentTimeMillis();
            System.out.println("Remote invocation completed in: " + (end-start) + " ms");

            System.out.println("MaxSize "+rpy.getMaxSize()+" word:"+rpy.getWord());
        } catch (RemoteException e) {
            e.printStackTrace();
        } catch (Exception ex) {
            System.err.println("Client unhandled exception: " + ex.toString());
            ex.printStackTrace();
        }
    }

     static List<String> LerNlinhas() throws IOException {
         System.out.println("Introduce text lines with separated words and finish with a blank line");
         BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
         List<String> lines=new ArrayList<String>();
         for (; ; ) {
             String line = br.readLine();
             if (line.length() == 0) break;
             lines.add(line);
         }
         return lines;
     }
}
