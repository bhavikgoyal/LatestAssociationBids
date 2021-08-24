using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Common
{
    public class Invoice :IDisposable
    {
        
        Document doc;
        PdfPTable tableLayout;
        string fileName = "Invoice" + System.DateTime.Now.Ticks.ToString();
        string path = "";   //Server.MapPath("~/content/" + fileName + ".pdf")
        public Int32 noOfColumn = 0;  //  7 

        public Invoice(string fName, Boolean rotate)
        {
            if (rotate) 
                doc = new Document(PageSize.LEGAL.Rotate());
            else
                doc = new Document(PageSize.LEGAL);

            if (!string.IsNullOrEmpty(fName))
                fileName = fName;

            noOfColumn = 0;
        }

        public Invoice()
        {

        }

        public void setLayout()
        {   //   
            //Create PDF Table
            tableLayout = new PdfPTable(noOfColumn);

            float[] headers = new float[noOfColumn]; //= { 14, 14, 14, 14, 14, 14, 14 };  //Header Widths


            for (int i = 0; i < headers.Length; i++)
            {
                headers[i] = new float();
                headers[i] = (float)(100 / noOfColumn);
            }

            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage


        }

        public void setLayoutForEngagement()
        {
            tableLayout = new PdfPTable(noOfColumn);
            float[] headers = new float[] { 6f, 6f, 6f, 6f, 8f, 15f, 10f, 10f, 7f, 8f, 8f, 10f };
            tableLayout.SetWidths(headers);
        }

        public void setLayoutForProspect()
        {
            tableLayout = new PdfPTable(noOfColumn);
            float[] headers = new float[] { 9f, 9f, 9f, 18f, 14f, 9f, 9f, 9f, 14f };
            tableLayout.SetWidths(headers);
        }

        public void createFile(string filePath)
        {
            path = filePath;
            //Create a PDF file in specific path
            PdfWriter.GetInstance(doc, new FileStream(path + fileName + ".pdf", FileMode.Create));
        }

        public void openDocument()
        {
            try
            {
                doc.Open();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void openDocument(string filename)
        {
            try
            {
                doc.Open();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PdfPCell getBlankRow()
        {
            //blank row
            PdfPCell blankrow = new PdfPCell(new Phrase("                 ")) { Colspan = noOfColumn, Border = 0, Padding = 0, HorizontalAlignment = Element.ALIGN_CENTER };
            blankrow.BackgroundColor = new BaseColor(255, 255, 255);
            return blankrow;
        }

        public Font getBoldFontWithSize9White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(255, 255, 255));
        }

        public Font getBoldFontWithSize8White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, new BaseColor(255, 255, 255));
        }

        public Font getNormalFontWithSize8White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, 8, new BaseColor(255, 255, 255));
        }

        public Font getBoldFontWithSize9Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize9BlackForMemo()
        {
            return FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize8Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, new BaseColor(0, 0, 0));
        }

        public Font getNormalFontWithSize8Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, 8, new BaseColor(0, 0, 0));
        }

        public Font getNormalFontWithSize8BlackForMemo()
        {
            return FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, new BaseColor(0, 0, 0));
        }

        public void setPageHeader(string PageHeader)
        {
            //Add Title to the PDF file at the top
            PdfPCell pageHeader = new PdfPCell(new Phrase(PageHeader, getBoldFontWithSize9White())) { Colspan = noOfColumn, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER };
            pageHeader.BackgroundColor = new BaseColor(23, 55, 94);
            tableLayout.AddCell(pageHeader);

            tableLayout.AddCell(getBlankRow());
        }

        public void setDate(string ShowDate)
        {
            PdfPCell date = new PdfPCell(new Phrase(ShowDate, getBoldFontWithSize8Black())) { Colspan = noOfColumn, Border = 0, PaddingBottom = 25, HorizontalAlignment = Element.ALIGN_RIGHT };
            date.HorizontalAlignment = Element.ALIGN_RIGHT;
            //date.BackgroundColor = new BaseColor(23, 55, 94);

            //date = PdfPCell(DateTime.Now.ToString());
            tableLayout.AddCell(date);

            tableLayout.AddCell(getBlankRow());
        }

       public void addContentTitle(string contentTitle)
        {
            //Add Section  Title to the PDF file at the top of header cell
            PdfPCell cell = new PdfPCell(new Phrase(contentTitle, getBoldFontWithSize8White())) { Colspan = noOfColumn, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER };
            cell.BackgroundColor = new BaseColor(85, 143, 213);
            tableLayout.AddCell(cell);
        }

        public void setTableHeaders(List<string> lst_Headers)
        {

            //Add header
            Int32 columnNo = 0;
            foreach (string headers in lst_Headers)
            {
                columnNo++;
                PdfPCell cell = new PdfPCell(new Phrase(headers, getBoldFontWithSize8Black())) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                // cell.BackgroundColor = BaseColor.RED;
                cell.Border = 0;
                tableLayout.AddCell(cell);
            }

        }

        public void addContentToBody(DataTable dt)
        {
            string body = "";
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    body = Convert.ToString(dr[dc]);
                    PdfPCell cell = new PdfPCell(new Phrase(body, getNormalFontWithSize8Black())) { Padding = 5, HorizontalAlignment = Element.ALIGN_JUSTIFIED };  //HorizontalAlignment = Element.ALIGN_LEFT,
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 3;
                    cell.BorderColorBottom = new BaseColor(0, 0, 0);
                    tableLayout.AddCell(cell);
                }
            }
        }

        public void addGrandTotalForActive(decimal grandTotal)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase("Total Probability Weighted Fees", getBoldFontWithSize8Black())) { Colspan = (noOfColumn - 1), HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5 };
            cell1.Border = 3;
            cell1.BorderColorBottom = new BaseColor(0, 0, 0);
            tableLayout.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase("" + grandTotal.ToString("N2"), getBoldFontWithSize8Black())) { Colspan = noOfColumn, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5 }; //$ removed
            cell2.Border = 3;
            cell2.BorderColorBottom = new BaseColor(0, 0, 0);
            tableLayout.AddCell(cell2);
        }

        public void addContentForMemos(List<string> headersList, List<string> Content)
        {
            addBlankRow();
            addBlankRow();
            for (int i = 0; i < headersList.Count; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(headersList[i], getBoldFontWithSize9BlackForMemo())) { PaddingTop = 5, Padding = 5, PaddingBottom = 5 };  //HorizontalAlignment = Element.ALIGN_LEFT,
                cell.Border = 0;
                //cell.BorderColorBottom = new BaseColor(0, 0, 0);
                tableLayout.AddCell(cell);

                cell = null;
                cell = new PdfPCell(new Phrase(Content[i], getNormalFontWithSize8BlackForMemo())) { PaddingTop = 5, Padding = 5, PaddingBottom = 5 };  //HorizontalAlignment = Element.ALIGN_LEFT,
                cell.Border = 0;
                //cell.BorderColorBottom = new BaseColor(0, 0, 0);
                tableLayout.AddCell(cell);
                //addBlankRow();
                addBlankRow();
            }
        }

        public void resetLayoutNAddNewPage()
        {
            doc.Add(tableLayout);
            doc.NewPage();
            tableLayout = new PdfPTable(noOfColumn);
            setLayout();
        }

        public void addBlankRow()
        {
            //blank row
            tableLayout.AddCell(getBlankRow());
        }

        public void addNewPage()
        {
            doc.NewPage();
        }

        public void closeDocument()
        {
            try
            {
                doc.Add(tableLayout);
                doc.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void getDocument()
        {
            try
            {
                Process.Start(path + fileName + ".pdf");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string getReportPath()
        {
            return path + fileName + ".pdf";
        }

        public string getReportName()
        {
            return fileName + ".pdf";
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }

    public class Invoicecollection 
    {
        Document doc;
        PdfPTable mainTable1;
        PdfPTable mainTable;

        string fileName = "Invoice" + System.DateTime.Now.Ticks.ToString();
        string path = "";   //Server.MapPath("~/content/" + fileName + ".pdf")

        public Invoicecollection()
        {
            //doc = new Document(PageSize.LEGAL.Rotate());
            doc = new Document(PageSize.A4);
            doc.SetMargins(15f, 0f, 40f, 15f);
            mainTable1 = new PdfPTable(1);
            mainTable = new PdfPTable(1);
            mainTable.SetWidths(new float[1] { 100 });
            mainTable.WidthPercentage = 100;
            mainTable.DefaultCell.Padding = 5;
            mainTable.DefaultCell.BorderWidth = 1;
            //mainTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            //mainTable.DefaultCell.Border = Rectangle.RIGHT_BORDER;
            //mainTable.DefaultCell.Border = Rectangle.LEFT_BORDER;
            mainTable.DefaultCell.BorderColor = BaseColor.BLACK;

            mainTable.DefaultCell.BorderWidthBottom = 1;
            mainTable.DefaultCell.BorderWidthTop = 1;
            mainTable.DefaultCell.BorderWidthLeft = 1;
            mainTable.DefaultCell.BorderWidthRight = 1;
        }

        //public void addHeaderToMainTable1(string pageTitle)
        //{
        //    PdfPCell pageHeader = new PdfPCell(new Phrase(pageTitle, getBoldFontWithSize10Black())) { Border = 0, Padding = 15, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
        //    pageHeader.BackgroundColor = new BaseColor(255, 255, 204);
        //    mainTable.AddCell(pageHeader);
        //}

        public Image checkedBox(string reportPath)
        {
         
            var imgChecked = Image.GetInstance(@"" + reportPath + "\\assets\\images\\brand\\logo.png");
            imgChecked.ScaleAbsoluteWidth(100F);
            imgChecked.ScaleAbsoluteHeight(30f);
            return imgChecked;
        }

        public void addHeaderCheckboxList2(string reportPath, string  date, int Vendorkey)
        {
            PdfPTable cbTbl = new PdfPTable(3);
            cbTbl.SetWidths(new float[3] { 50, 20, 30 });


            cbTbl.DefaultCell.Border = 0;

            cbTbl.AddCell(new PdfPCell(checkedBox(reportPath)) { Padding = 5, Border = 0 });

            cbTbl.AddCell(new PdfPCell() { Padding = 5, Border = 0 });

            cbTbl.AddCell(new PdfPCell(new Phrase("Invoice: "+ Vendorkey, getBoldFontWithSizeivoice())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
            cbTbl.AddCell(new PdfPCell() { Padding = 5, Border = 0 });
            cbTbl.AddCell(new PdfPCell() { Padding = 5, Border = 0 });
            cbTbl.AddCell(new PdfPCell(new Phrase("Invoice Date :"+ date, getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });

            cbTbl.AddCell(new PdfPCell() { Padding = 5, Border = 0 });
            cbTbl.AddCell(new PdfPCell() { Padding = 5, Border = 0 });
            cbTbl.AddCell(new PdfPCell(new Phrase("Due Date: "+ date, getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });

            mainTable.AddCell(new PdfPCell(cbTbl) { Border = 0, PaddingTop = 10, PaddingBottom = 10, VerticalAlignment = Element.ALIGN_CENTER, HorizontalAlignment = Element.ALIGN_CENTER });
            
        }


        public void horizontalline()
        {
            PdfPTable cbTbl = new PdfPTable(1);
            cbTbl.SetWidths(new float[1] { 80 });

          

            Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_RIGHT, 1)));
            // Set gap between line paragraphs.
            lineSeparator.SetLeading(0.5F, 0.5F);
            Paragraph par = new Paragraph(" ");
            // Set gap between text paragraphs.
            par.SetLeading(0.7F, 0.7F);


            cbTbl.DefaultCell.Border = 0;

         

            cbTbl.AddCell(lineSeparator);

           
            mainTable.AddCell(new PdfPCell(cbTbl) { Border = 0, PaddingTop = 10, PaddingBottom = 10, VerticalAlignment = Element.ALIGN_CENTER, HorizontalAlignment = Element.ALIGN_CENTER });

        }

        public void addReciptDetailsTable3(  string fromemail, string addressline1, string addressline2, string zip, string City, string state)
        {
            PdfPTable tblReceiptDet = new PdfPTable(3);
            tblReceiptDet.WidthPercentage = 100;
            tblReceiptDet.SetWidths(new float[3] {30,40, 30 });
            tblReceiptDet.DefaultCell.Border = 0;

            //Row 1

            PdfPCell cell1 = new PdfPCell(new Phrase("Invoice From:", getBoldFontWithSizeivoice())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell1);


            tblReceiptDet.AddCell(getBlankRow());

            PdfPCell cell3 = new PdfPCell(new Phrase("Invoice To:", getBoldFontWithSizeivoice())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell3);

          




            PdfPCell cell11 = new PdfPCell(new Phrase("Head Office"+"Association Bids", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell11);

         

            tblReceiptDet.AddCell(getBlankRow());

            PdfPCell cell13 = new PdfPCell(new Phrase("Street , "+ addressline2, getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell13);

           
            //Row 2
            PdfPCell cell21 = new PdfPCell(new Phrase("Paris", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell21);

           

            tblReceiptDet.AddCell(getBlankRow());

            PdfPCell cell41 = new PdfPCell(new Phrase(state + City, getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell41);


          




            PdfPCell cell61 = new PdfPCell(new Phrase("Association Bids Team", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell61);
           
            tblReceiptDet.AddCell(getBlankRow());

            PdfPCell cell81 = new PdfPCell(new Phrase(fromemail, getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            tblReceiptDet.AddCell(cell81);


  

            //Row 3
            tblReceiptDet.AddCell(getBlankRow());

            tblReceiptDet.AddCell(getBlankRow());

            tblReceiptDet.AddCell(getBlankRow());
            tblReceiptDet.AddCell(getBlankRow());

            tblReceiptDet.AddCell(getBlankRow());


            mainTable.AddCell(new PdfPCell(tblReceiptDet) { PaddingLeft = 5, Border = 0, BorderWidth = 0, PaddingBottom = 10 });
            //mainTable.AddCell(tblReceiptDet);
        }






        public void addreciptdatefor4(string Title, int Amount)
        {
            PdfPTable table = new PdfPTable(8);
            table.DefaultCell.Padding = 2;
            table.WidthPercentage = 80f;

            PdfPCell cell = new PdfPCell(new Phrase("Item", getBoldFontWithSize10Black())) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            PdfPCell cell22 = new PdfPCell(new Phrase("", getBoldFontWithSize10Black())) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER };
            cell22.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(cell22);

            cell.Colspan = 6;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(cell);
          
           

            PdfPCell cell63 = new PdfPCell(new Phrase("Price", getBoldFontWithSize9Black())) {  HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            cell63.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(cell63);
          

            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            PdfPCell cell2 = new PdfPCell(new Phrase(Title, getNormalFontWithSize8Black())) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            PdfPCell cell64 = new PdfPCell(new Phrase("1", getBoldFontWithSize9Black())) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            table.AddCell(cell64);         
            cell2.Colspan = 6;
            table.AddCell(cell2);
            PdfPCell cell65 = new PdfPCell(new Phrase("$"+ Amount, getNormalFontWithSize8Black())) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            table.AddCell(cell65);
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            PdfPCell cell3 = new PdfPCell(new Phrase("Total", getBoldFontWithSize9Black())) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_CENTER };
            
            cell3.Colspan = 7;
            table.AddCell(cell3);

            PdfPCell cell4 = new PdfPCell(new Phrase("$"+Amount, getNormalFontWithSize8Black())) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            table.AddCell(cell4);          
            cell.HorizontalAlignment = 1;



            mainTable.AddCell(new PdfPCell(table) { PaddingLeft = 5, Border = 0, BorderWidth = 0, PaddingBottom = 10 });


            PdfPTable cbTbl = new PdfPTable(1);
            cbTbl.SetWidths(new float[1] { 80 });



            Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.WHITE, Element.ALIGN_RIGHT, 1)));
          
           lineSeparator.SetLeading(0.5F, 0.5F);
            Paragraph par = new Paragraph(" ");
           
           par.SetLeading(0.7F, 0.7F);


            cbTbl.DefaultCell.Border = 0;



            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);
            cbTbl.AddCell(lineSeparator);

            mainTable.AddCell(new PdfPCell(cbTbl) { Border = 0, PaddingTop = 10, PaddingBottom = 10, VerticalAlignment = Element.ALIGN_CENTER, HorizontalAlignment = Element.ALIGN_CENTER });


        }

        //public void addReciptAmountDetailTable5(string reportPath, string Amount, string PaymentMode, string Notes, string Discount, string Penalty, string BasicAmount)
        //{
        //    PdfPTable tblReceiptAmt = new PdfPTable(3);
        //    tblReceiptAmt.SetWidths(new float[3] { 35, 15, 50 });
        //    tblReceiptAmt.WidthPercentage = 40f;
        //    tblReceiptAmt.DefaultCell.Border = 0;
        //    //Table left
        //    PdfPTable tblRecAmt = new PdfPTable(2);
        //    tblRecAmt.SetWidths(new float[2] { 50, 50 });
        //    tblRecAmt.DefaultCell.Border = 0;

        //    tblRecAmt.AddCell(new PdfPCell(new Phrase("Basic Amount:", getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase(" Rs " + BasicAmount + "/-", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase("Discount:", getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase(" Rs " + Discount + "/-", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase("Penalty:", getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase(" Rs " + Penalty + "/-", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase("Final/Net Amount:", getBoldFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });
        //    tblRecAmt.AddCell(new PdfPCell(new Phrase(" Rs " + Amount + "/-", getNormalFontWithSize8Black())) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_CENTER });

        //    //Table Right
        //    PdfPTable tblRecAmtType = new PdfPTable(5);
        //    tblRecAmtType.SetWidths(new float[5] { 20, 20, 20, 20, 20 });
        //    tblRecAmtType.WidthPercentage = 40f;
        //    tblRecAmtType.DefaultCell.Border = 0;
        //    if (PaymentMode == "Cash")
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Cash", true)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    else
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Cash", false)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }

        //    if (PaymentMode == "Cheque")
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Cheque", true)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    else
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Cheque", false)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }

        //    if (PaymentMode == "Online")
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Online", true)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    else
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Online", false)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }

        //    if (PaymentMode == "Other")
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Other", true)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    else
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Other", false)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    if (PaymentMode == "Paytm")
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Paytm", true)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }
        //    else
        //    {
        //        tblRecAmtType.AddCell(new PdfPCell(checkedBox(reportPath, "Paytm", false)) { Padding = 1, PaddingBottom = 5, Border = 0 });
        //    }



        //    tblRecAmtType.AddCell(new PdfPCell(new Phrase(Notes, getNormalFontWithSize8Black())) { Colspan = 5, Border = 0, BorderWidthTop = 1, PaddingTop = 5, HorizontalAlignment = Element.ALIGN_LEFT });

        //    //tables add to parent table
        //    tblReceiptAmt.AddCell(new PdfPCell(tblRecAmt) { Padding = 10 });
        //    //tblReceiptAmt.AddCell(tblRecAmt);
        //    tblReceiptAmt.AddCell(new PdfPCell(new Phrase("  ")) { Border = 0, Padding = 0 });
        //    tblReceiptAmt.AddCell(new PdfPCell(tblRecAmtType) { Padding = 5 });
        //    tblReceiptAmt.AddCell(new PdfPCell(tblRecAmtType) { Padding = 5 });
        //    //tblReceiptAmt.AddCell(tblRecAmtType);
        //    //tables add to parent table
        //    mainTable.AddCell(new PdfPCell(tblReceiptAmt) { Border = 0, PaddingTop = 10, PaddingBottom = 10 });
        //    //mainTable.AddCell(tblReceiptAmt);
        //}

        //public void addReciptAmountTypeDetailTable6()
        //{

        //}



        public DataTable ConvertDataReaderToDataTable(IDataReader dr)
        {
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

     

        public void addFootercontent8()
        {
            PdfPTable tblRecNotes = new PdfPTable(5);
            tblRecNotes.SetWidths(new float[5] { 5, 30, 30, 30, 5 });
            tblRecNotes.DefaultCell.Border = 0;

        
           
            mainTable.AddCell(new PdfPCell(tblRecNotes) { Border = 0, PaddingTop = 10, PaddingBottom = 10 });

            string cont = "Thank you for the recent payment you have made to us. Let us know if u have any query.";
            mainTable.AddCell(new PdfPCell(new Phrase(cont, getNormalFontWithSize8Black())) { Border = 0, PaddingTop = 10, PaddingBottom = 15, PaddingLeft = 40, HorizontalAlignment = Element.ALIGN_CENTER });
        }

        public void addFooterRow9()
        {
           PdfPCell cell = new PdfPCell(new Phrase("---Copyright © 2020 Association Bids. All rights reserved---", getNormalFontWithSize8Black())) { Border = 1, Padding = 10, HorizontalAlignment = Element.ALIGN_CENTER };
           cell.BackgroundColor = new BaseColor(247, 247, 247);
           mainTable.AddCell(cell);
        }

        public void createFile(string filePath)
        {
            path = filePath;
            //Create a PDF file in specific path
            PdfWriter.GetInstance(doc, new FileStream(path + fileName + ".pdf", FileMode.Create));
        }

        public string getFilename()
        {
            return fileName + ".pdf";
        }

        public void openDocument()
        {
            try
            {
                doc.Open();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void closeDocument()
        {
            try
            {
                mainTable1.AddCell(new PdfPCell(mainTable) { BorderWidth = 1, BorderColor = BaseColor.BLACK });
                doc.Add(mainTable1);
                doc.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void getDocument()
        {
            try
            {
                Process.Start(path + fileName + ".pdf");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string getReportPath()
        {
            return path + fileName + ".pdf";
        }

        public string getReportName()
        {
            return fileName + ".pdf";
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

        public Font getBoldFontWithSize9White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(255, 255, 255));
        }

        public Font getBoldFontWithSize8White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, new BaseColor(255, 255, 255));
        }

        public Font getNormalFontWithSize8White()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, 8, new BaseColor(255, 255, 255));
        }

        public Font getBoldFontWithSize9Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize10Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize11Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize9BlackForMemo()
        {
            return FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSize8Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, new BaseColor(0, 0, 0));
        }

        public Font getBoldFontWithSizeivoice()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15, new BaseColor(0, 0, 0));
        }

        public Font getNormalFontWithSize8Black()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, 8, new BaseColor(0, 0, 0));
        }

        public Font getNormalFontWithSize8BlackForMemo()
        {
            return FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, new BaseColor(0, 0, 0));
        }


        public PdfPCell getBlankRow()
        {
            //blank row
            PdfPCell blankrow = new PdfPCell(new Phrase("  ")) { Border = 0, Padding = 0, HorizontalAlignment = Element.ALIGN_CENTER };
            //blankrow.BackgroundColor = new BaseColor(255, 255, 255);
            return blankrow;
        }

    }



    public class DynamicCheckbox : IPdfPCellEvent
        {
            private string fieldname;

            public DynamicCheckbox(string name)
            {
                fieldname = name;
            }

            public void CellLayout(PdfPCell cell, Rectangle rectangle, PdfContentByte[] canvases)
            {
                PdfWriter writer = canvases[0].PdfWriter;
                rectangle.BackgroundColor = BaseColor.RED;
                rectangle.BorderColor = BaseColor.RED;
                RadioCheckField ckbx = new RadioCheckField(writer, rectangle, fieldname, "Yes");
                ckbx.CheckType = RadioCheckField.TYPE_CHECK;
                ckbx.BackgroundColor = BaseColor.ORANGE;
                ckbx.FontSize = 8;

                ckbx.BorderColor = BaseColor.GREEN;
                ckbx.Text = fieldname;
                ckbx.TextColor = BaseColor.BLACK;
                PdfFormField field = ckbx.CheckField;
                writer.AddAnnotation(field);

            }



        }
    }
