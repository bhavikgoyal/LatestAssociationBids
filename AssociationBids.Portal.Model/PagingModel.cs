using System;

namespace AssociationBids.Portal.Model
{
    public class PagingModel
    {
        #region Private Variables
        private int __pageNumberStart;
        private int __pageNumberEnd;
        private int __totalPageCount;
        private int __totalRecordCount;
        private int __pageRecordStart;
        private int __pageRecordEnd;
        #endregion

        public PagingModel()
        {
            PageNumber = 1;
            PageSize = 10;
            MaximumPageNumbers = 5;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int MaximumPageNumbers { get; set; }
        public string SortOrder { get; set; }

        public int TotalRecordCount
        {
            get { return __totalRecordCount; }
            set
            {
                __totalRecordCount = value;

                // Reset values to be calculated
                __pageNumberStart = 0;
                __pageNumberEnd = 0;
                __totalPageCount = 0;
                __pageRecordStart = 0;
                __pageRecordEnd = 0;
            }
        }

        #region Calculated Properties
        public int PageNumberStart
        {
            get
            {
                if (__pageNumberStart == 0)
                {
                    __pageNumberStart = 1;

                    if (PageNumber > MaximumPageNumbers)
                    {
                        __pageNumberStart = (PageNumber / MaximumPageNumbers);

                        if ((PageNumber % MaximumPageNumbers) == 0)
                        {
                            __pageNumberStart = ((__pageNumberStart - 1) * MaximumPageNumbers) + 1;
                        }
                        else
                        {
                            __pageNumberStart = (__pageNumberStart * MaximumPageNumbers) + 1;
                        }
                    }
                }
                return __pageNumberStart;
            }
        }

        public int PageNumberEnd
        {
            get
            {
                if (__pageNumberEnd == 0)
                {
                    __pageNumberEnd = PageNumberStart + MaximumPageNumbers - 1;
                    __pageNumberEnd = (__pageNumberEnd < TotalPageCount ? __pageNumberEnd : TotalPageCount);
                }
                return __pageNumberEnd;
            }
        }

        public int TotalPageCount
        {
            get
            {
                if (__totalPageCount == 0)
                {
                    __totalPageCount = (TotalRecordCount / PageSize);

                    if ((TotalRecordCount % PageSize) > 0)
                    {
                        __totalPageCount++;
                    }
                }
                return __totalPageCount;
            }
        }

        public int PageRecordStart
        {
            get
            {
                if (__pageRecordStart == 0)
                {
                    __pageRecordStart = 1;

                    if (PageNumber > 1)
                    {
                        __pageRecordStart = ((PageNumber - 1) * PageSize) + 1;
                    }
                }
                return __pageRecordStart;
            }
        }

        public int PageRecordEnd
        {
            get
            {
                if (__pageRecordEnd == 0)
                {
                    __pageRecordEnd = PageRecordStart + PageSize - 1;

                    if (__pageRecordEnd > TotalRecordCount)
                    {
                        __pageRecordEnd = TotalRecordCount;
                    }
                }
                return __pageRecordEnd;
            }
        }
        #endregion
    }
}
