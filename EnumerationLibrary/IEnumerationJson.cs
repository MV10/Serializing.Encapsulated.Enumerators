namespace EnumerationLibrary
{
    public interface IEnumerationJson
    {
        object ReadJson(string jsonValue);
        string WriteJson();
    }
}
