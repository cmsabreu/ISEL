package functionhttp;


import com.google.cloud.functions.HttpFunction;
import com.google.cloud.functions.HttpRequest;
import com.google.cloud.functions.HttpResponse;

import java.io.BufferedWriter;

public class Entrypoint implements HttpFunction {
    @Override
    public void service(HttpRequest request, HttpResponse response) throws Exception {
        BufferedWriter writer = response.getWriter();
        String name=request.getFirstQueryParameter("name").orElse("World");
        writer.write("Hello "+name+" from Cloud Function triggered by http");
    }
}
