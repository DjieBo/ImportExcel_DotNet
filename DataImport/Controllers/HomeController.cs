using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using DataImport.Models;

namespace DataImport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ImportKota(FormCollection formCollection)
        {
            var dataList = new List<Lokasi>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadKota"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var listData = new Lokasi();
                            listData.ID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            listData.IDProvinsi = workSheet.Cells[rowIterator, 2].Value.ToString();
                            listData.Provinsi = workSheet.Cells[rowIterator, 3].Value.ToString();
                            listData.Kota = workSheet.Cells[rowIterator, 4].Value.ToString();
                            dataList.Add(listData);
                        }
                    }
                }
            }
            using (BuildingContext importDB = new BuildingContext())
            {
                foreach (var item in dataList)
                {
                    importDB.Lokasi.Add(item);
                }
                importDB.SaveChanges();
            }
            return View("Index");
        }
    }
}