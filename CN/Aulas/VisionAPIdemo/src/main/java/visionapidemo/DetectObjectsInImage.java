package visionapidemo;

import com.google.cloud.ReadChannel;
import com.google.cloud.WriteChannel;
import com.google.cloud.storage.*;
import com.google.cloud.vision.v1.Image;
import com.google.cloud.vision.v1.*;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.channels.Channels;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class DetectObjectsInImage {


    public static void main(String[] args) throws IOException {
        try {
            // Depende da variavel GOOGLE_APPLICATION_CREDENTIALS
            // com conta de serviço  de acesso ao serviço Storage com role "Storage Admin"
            if (args.length != 1) {
                System.out.println("Usage: java -jar DetectObjects....jar <bucket name with images>");
                System.exit(-1);
            }
            String bucketName = args[0];
            StorageOptions storageOptions = StorageOptions.getDefaultInstance();
            Storage storage = storageOptions.getService();
            String blobName = listBucketChooseImage(storage, bucketName);
            detectLocalizedObjectsGcs(storage, bucketName, blobName);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public static String listBucketChooseImage(Storage storage, String bucketName) {
        Bucket bucket = storage.get(bucketName);
        for (Blob blob : bucket.list().iterateAll()) {
            System.out.println("    " + blob.toString());
        }
        System.out.println("\n  Enter Blob Name? ");
        Scanner scan = new Scanner(System.in);
        return scan.nextLine();
    }

    public static void detectLocalizedObjectsGcs(Storage storage, String bucketName, String blobName) throws IOException {

        String gcsPath = "gs://" + bucketName + "/" + blobName;
        ImageSource imgSource = ImageSource.newBuilder().setGcsImageUri(gcsPath).build();
        Image img = Image.newBuilder().setSource(imgSource).build();

        AnnotateImageRequest request =
                AnnotateImageRequest.newBuilder()
                        .addFeatures(Feature.newBuilder().setType(Feature.Type.OBJECT_LOCALIZATION))
                        .setImage(img)
                        .build();

        BatchAnnotateImagesRequest singleBatchRequest = BatchAnnotateImagesRequest.newBuilder()
                .addRequests(request)
                .build();

        try (ImageAnnotatorClient client = ImageAnnotatorClient.create()) {
            // Perform the request
            BatchAnnotateImagesResponse batchResponse = client.batchAnnotateImages(singleBatchRequest);
            List<AnnotateImageResponse> listResponses = batchResponse.getResponsesList();

            if (listResponses.isEmpty()) {
                System.out.println("Empty response, no object detected.");
                return;
            }
            // get the only response
            AnnotateImageResponse response = listResponses.get(0);
            // print information in standard output
            for (LocalizedObjectAnnotation annotation : response.getLocalizedObjectAnnotationsList()) {
                System.out.format("Object name: %s%n", annotation.getName());
                System.out.format("Confidence: %s%n", annotation.getScore());
                System.out.format("Normalized Vertices:%n");
                annotation
                        .getBoundingPoly()
                        .getNormalizedVerticesList()
                        .forEach(vertex -> System.out.println("(" + vertex.getX() + ", " + vertex.getY() + ")"));
            }
            // annotate in memory Blob image
            BufferedImage bufferImg = getBlobBufferedImage(storage, BlobId.of(bucketName, blobName));
            annotateWithObjects(bufferImg, response.getLocalizedObjectAnnotationsList());
            // save the image to a new blob in the same bucket. The name of new blob has the annotated prefix
            writeAnnotatedImage(storage, bufferImg, bucketName, "annotated-" + blobName);
        }
    }

    private static void writeAnnotatedImage(Storage storage, BufferedImage bufferImg, String bucketName, String destinationBlobName) throws IOException {
        BlobInfo blobInfo = BlobInfo
                .newBuilder(BlobId.of(bucketName, destinationBlobName))
                .setContentType("image/jpeg")
                .build();
        Blob destBlob = storage.create(blobInfo);
        WriteChannel writeChannel = storage.writer(destBlob);
        OutputStream out = Channels.newOutputStream(writeChannel);
        ImageIO.write(bufferImg, "jpg", out);
        out.close();
    }

    private static BufferedImage getBlobBufferedImage(Storage storage, BlobId blobId) throws IOException {
        Blob blob = storage.get(blobId);
        if (blob == null) {
            System.out.println("No such Blob exists !");
            throw new IOException("Blob <" + blobId.getName() + "> not found in bucket <" + blobId.getBucket() + ">");
        }
        ReadChannel reader = blob.reader();
        InputStream in = Channels.newInputStream(reader);
        return ImageIO.read(in);
    }

    public static void annotateWithObjects(BufferedImage img, List<LocalizedObjectAnnotation> objects) {
        for (LocalizedObjectAnnotation obj : objects) {
            annotateWithObject(img, obj);
        }
    }

    private static void annotateWithObject(BufferedImage img, LocalizedObjectAnnotation obj) {
        Graphics2D gfx = img.createGraphics();
        gfx.setFont(new Font("Arial", Font.PLAIN, 18));
        gfx.setStroke(new BasicStroke(3));
        gfx.setColor(new Color(0x00ff00));
        Polygon poly = new Polygon();
        BoundingPoly imgPoly = obj.getBoundingPoly();
        // draw object name
        gfx.drawString(obj.getName(),
                imgPoly.getNormalizedVertices(0).getX() * img.getWidth(),
                imgPoly.getNormalizedVertices(0).getY() * img.getHeight() - 3);
        // draw bounding box of object
        for (NormalizedVertex vertex : obj.getBoundingPoly().getNormalizedVerticesList()) {
            poly.addPoint((int) (img.getWidth() * vertex.getX()), (int) (img.getHeight() * vertex.getY()));
        }
        gfx.draw(poly);
    }
}

