//
// Diese Datei wurde mit der JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.8-b130911.1802 generiert 
// Siehe <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Änderungen an dieser Datei gehen bei einer Neukompilierung des Quellschemas verloren. 
// Generiert: 2018.06.10 um 08:22:25 PM CEST 
//


package sigence.scenario.tool.mockup.generated;

import java.math.BigDecimal;
import java.math.BigInteger;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für anonymous complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="PrimaryKey" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="Id" type="{http://www.w3.org/2001/XMLSchema}integer"/>
 *         &lt;element name="Latitude" type="{http://www.w3.org/2001/XMLSchema}decimal"/>
 *         &lt;element name="Longitude" type="{http://www.w3.org/2001/XMLSchema}decimal"/>
 *         &lt;element name="Altitude" type="{http://www.w3.org/2001/XMLSchema}integer"/>
 *         &lt;element name="LocalizationTime" type="{http://www.w3.org/2001/XMLSchema}decimal"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "primaryKey",
    "id",
    "latitude",
    "longitude",
    "altitude",
    "localizationTime"
})
@XmlRootElement(name = "GeoLocalizationResult")
public class GeoLocalizationResult {

    @XmlElement(name = "PrimaryKey", required = true)
    protected String primaryKey;
    @XmlElement(name = "Id", required = true)
    protected BigInteger id;
    @XmlElement(name = "Latitude", required = true)
    protected BigDecimal latitude;
    @XmlElement(name = "Longitude", required = true)
    protected BigDecimal longitude;
    @XmlElement(name = "Altitude", required = true)
    protected BigInteger altitude;
    @XmlElement(name = "LocalizationTime", required = true)
    protected BigDecimal localizationTime;

    /**
     * Ruft den Wert der primaryKey-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPrimaryKey() {
        return primaryKey;
    }

    /**
     * Legt den Wert der primaryKey-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPrimaryKey(String value) {
        this.primaryKey = value;
    }

    /**
     * Ruft den Wert der id-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link BigInteger }
     *     
     */
    public BigInteger getId() {
        return id;
    }

    /**
     * Legt den Wert der id-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link BigInteger }
     *     
     */
    public void setId(BigInteger value) {
        this.id = value;
    }

    /**
     * Ruft den Wert der latitude-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link BigDecimal }
     *     
     */
    public BigDecimal getLatitude() {
        return latitude;
    }

    /**
     * Legt den Wert der latitude-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link BigDecimal }
     *     
     */
    public void setLatitude(BigDecimal value) {
        this.latitude = value;
    }

    /**
     * Ruft den Wert der longitude-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link BigDecimal }
     *     
     */
    public BigDecimal getLongitude() {
        return longitude;
    }

    /**
     * Legt den Wert der longitude-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link BigDecimal }
     *     
     */
    public void setLongitude(BigDecimal value) {
        this.longitude = value;
    }

    /**
     * Ruft den Wert der altitude-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link BigInteger }
     *     
     */
    public BigInteger getAltitude() {
        return altitude;
    }

    /**
     * Legt den Wert der altitude-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link BigInteger }
     *     
     */
    public void setAltitude(BigInteger value) {
        this.altitude = value;
    }

    /**
     * Ruft den Wert der localizationTime-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link BigDecimal }
     *     
     */
    public BigDecimal getLocalizationTime() {
        return localizationTime;
    }

    /**
     * Legt den Wert der localizationTime-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link BigDecimal }
     *     
     */
    public void setLocalizationTime(BigDecimal value) {
        this.localizationTime = value;
    }

}
