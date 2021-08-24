using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IPMVendorService : IBaseService
    {
        bool Validate(VendorModel item);

        bool IsFilterEnabled(VendorFilterModel filter);

        VendorFilterModel CreateFilter();

        VendorModel Get(int id);

        IList<VendorModel> GetAll();

        Int64 Insert(VendorModel vendorModel);

        IList<VendorModel> GetAll(VendorFilterModel filter);

        List<VendorModel> SearchVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort);

        List<VendorModel> Searchinsurance(int CompanyKey);

        IList<VendorModel> GetAllState();

        IList<VendorModel> GetAllService();

        IList<VendorModel> GetAllProperty();

        VendorModel GetDataViewEdit(int id);

        Int64 VendorEdit(VendorModel item);

        bool insuranceEdit(VendorModel item);

        IList<VendorModel> Getbindservice(int CompanyKey);

        bool ServicestDelete(int CompanyKey, string sername);

        IList<VendorModel> GetbindDocument(int CompanyKey);
        bool CheckDuplicatedEmail(string Email);

        bool Remove(int CompanyKey);

    }
}
