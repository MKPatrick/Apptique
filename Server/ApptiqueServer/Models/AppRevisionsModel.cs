namespace ApptiqueServer.Models
{
    public class AppRevisionsModel
    {
        public int ID { get; set; }
        public int AppVersion { get; set; }
        public string ApkPath { get; set; }
        public string AppVersionName { get; set; }
        public string ChangeSet { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
