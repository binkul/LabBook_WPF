namespace LabBook.Forms.Navigation
{
    public interface INavigation
    {
        long DgRowIndex { get; set; }

        long GetRowCount { get; }

        void Refresh();
    }
}
