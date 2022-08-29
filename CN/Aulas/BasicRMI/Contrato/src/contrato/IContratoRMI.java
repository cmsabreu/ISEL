package contrato;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IContratoRMI extends Remote {

    Reply doRemoteWork(Request req) throws RemoteException;

}
