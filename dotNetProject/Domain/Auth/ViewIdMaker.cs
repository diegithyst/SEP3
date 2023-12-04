namespace Domain.Auth;

public static class ViewIdMaker
{
    public static int makeId(long id)
    {
        int temp = 0;
        temp = (int)id;
        int multiplier = (int)548053479;
        temp = temp * multiplier;
        string stringToList = temp.ToString();
        List<char> stringList = new List<char>();
        stringList.Add(stringToList[0]);
        stringList.Add(stringToList[1]);
        stringList.Add(stringToList[2]);
        stringList.Add(stringToList[3]);
        stringList.Add(stringToList[4]);
        string concat = "";
        concat = concat + stringList[0];
        concat = concat + stringList[1];
        concat = concat + stringList[2];
        concat = concat + stringList[3];
        concat = concat + stringList[4];
        int finalId = int.Parse(concat);
        return finalId;
    }
    
}