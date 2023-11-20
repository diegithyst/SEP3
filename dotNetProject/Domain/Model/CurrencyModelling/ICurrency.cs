namespace Domain.Model;

public interface ICurrency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }

    public void convertToEuro();
    public void convertToKrone();
    public void convertToPound();

}