namespace MyDoctorAppointment.Data.Interfaces
{
    public interface ISerializationService
    {
        void Serialize<T>(string filePath, T data);

        T Deserialize<T>(string filePath);
    }
}