package server;

import contrato.IContratoRMI;
import contrato.Reply;
import contrato.Request;

import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;
import java.util.Properties;

public class ServerRMI implements IContratoRMI {
    static String serverIP="localhost";
    static int registerPort = 7000;
    static int svcPort = 7001;
    static ServerRMI svc=null;

    public static void main(String[] args) {
        try {
            Properties props = System.getProperties();
            props.put("java.rmi.server.hostname", serverIP);

            svc = new ServerRMI();
            IContratoRMI stubSvc = (IContratoRMI) UnicastRemoteObject.exportObject(svc, svcPort);
            Registry registry = LocateRegistry.createRegistry(registerPort);

            registry.rebind("RemServer", stubSvc);  //regista skeleton com nome lógico

            System.out.println("Server ready: Press any key to finish server");
            java.util.Scanner scanner = new java.util.Scanner(System.in);
            String line = scanner.nextLine(); System.exit(0);
        } catch (RemoteException e) {
            e.printStackTrace();
        } catch (Exception ex) {
            System.err.println("Server unhandled exception: " + ex.toString());
            ex.printStackTrace();
        }
    }

    @Override
    public Reply doRemoteWork(Request request) throws RemoteException {
        int size = 0; String bigWord = "";
        for (String line : request.getTextLines()) {
            String[] tmp = line.split(" ");
            for (String s : tmp) {
                if (s.length() > size) {
                    size = s.length();  bigWord = s;
                }
            }
        }
        // simula tempo de processamento
        try {
            Thread.sleep(10*1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        Reply rpy=new Reply();
        rpy.setWord(bigWord); rpy.setMaxSize(size);
        return rpy;
    }
}
