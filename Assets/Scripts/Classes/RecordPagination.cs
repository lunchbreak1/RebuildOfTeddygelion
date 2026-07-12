using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordPagination : MonoBehaviour
{
    [SerializeField] private int recordsPerPage = 10;

    public List<SaveDataRecord> records = new List<SaveDataRecord>();
    private int currentPage = 0;
    [SerializeField] TextMeshProUGUI pageNumberDisplay;

    private void Start()
    {
        PaginateRecords();
    }

    public void PaginateRecords()
    {
        if (records != null)
        {
            records.Clear();
        }

        records.AddRange(GetComponentsInChildren<SaveDataRecord>(true));

        ShowPage(0);
    }

    public void NextPage()
    {
        int maxPage = (records.Count - 1) / recordsPerPage;

        if (currentPage < maxPage)
        {
            ShowPage(currentPage + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            ShowPage(currentPage - 1);
        }
    }

    public void ShowPage(int page)
    {
        int maxPage = (records.Count - 1) / recordsPerPage;
        currentPage = Mathf.Clamp(page, 0, maxPage);

        int startIndex = currentPage * recordsPerPage;
        int endIndex = startIndex + recordsPerPage;

        for (int i = 0; i < records.Count; i++)
        {
            bool shouldShow = i >= startIndex && i < endIndex;
            records[i].gameObject.SetActive(shouldShow);
        }

        int pageNumber = page+1;
        int maxPageNumber = maxPage+1;

        pageNumberDisplay.text = "Page " + pageNumber + " of " + maxPageNumber;
    }
}