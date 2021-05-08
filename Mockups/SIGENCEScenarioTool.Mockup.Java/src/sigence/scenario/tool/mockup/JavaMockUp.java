package sigence.scenario.tool.mockup;

import java.io.IOException;
import java.io.StringWriter;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.UUID;

import javax.xml.bind.JAXB;

import sigence.scenario.tool.mockup.generated.GeoLocalizationResult;

public class JavaMockUp {

	public static void main(String[] args) {

		try {

			InetAddress ia = InetAddress.getByName("127.0.0.1");
			int port = 7474;

			GeoLocalizationResult gcr = new GeoLocalizationResult();
			gcr.setPrimaryKey(UUID.randomUUID().toString());
			gcr.setId(BigInteger.valueOf(0));
			gcr.setLatitude(BigDecimal.valueOf(49.12345678));
			gcr.setLongitude(BigDecimal.valueOf(6.12345678));
			gcr.setAltitude(BigInteger.valueOf(0));
			gcr.setLocalizationTime(BigDecimal.valueOf(-1));

			StringWriter sw = new StringWriter();
			JAXB.marshal(gcr, sw);

			String strXml = sw.toString();
			System.out.println(strXml);

			byte[] data = strXml.getBytes();

			DatagramPacket packet = new DatagramPacket(data, data.length, ia, port);

			DatagramSocket toSocket = new DatagramSocket();
			toSocket.send(packet);

			toSocket.close();

			System.out.println("Successful send data to 127.0.0.1:7474 ...");

		} catch (UnknownHostException ex) {

			System.err.println(ex.getMessage());
		} catch (IOException ex) {

			System.err.println(ex.getMessage());
		}
	}
}
