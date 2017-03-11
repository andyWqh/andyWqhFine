/*********************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace andyWqhCommon.Excel
{
    public class NPOIExcel
    {
        private string _title;
        private string _sheetName;
        private string _filePath;

        /// <summary>
        /// DataTable导出Excel
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public bool ToExcel(DataTable table)
        {
            if (table == null)
            {
                return false;
            }
            FileStream fs = new FileStream(this
                ._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //创建工作簿
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = string.IsNullOrEmpty(this._sheetName) ? "sheet1" : this._sheetName;
            //创建sheet
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //创建表格标题行 即第一行
            IRow row = sheet.CreateRow(0);
            //创建一列并赋值
            row.CreateCell(0).SetCellValue(this._title);
            //合并单元格
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));

            row.Height = 500;
            //创建样式对象
            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            try
            {
                //处理表格列标题
                row = sheet.CreateRow(1);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Height = 350;
                    sheet.AutoSizeColumn(i);
                }

                //处理数据内容
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i + 2);
                    row.Height = 250;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(table.Rows[i][j] + "");
                        sheet.SetColumnWidth(j, 256 * 15);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();
            return true;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="title">标题</param>
        /// <param name="sheetName">文件名称</param>
        /// <returns></returns>
        public bool ToExcel(DataTable table, string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcel(table);
        }
    }
}
