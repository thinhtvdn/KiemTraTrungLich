using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Reflection;


namespace SendEmailVBC
{
    public partial class Form1 : Form
    {
        DataSet oDs = new DataSet();
        string tenTableThongtinGV = "lichBaoVeGV";
        string tencotGT1 = "Giám thị 1";
        string tencotGT2 = "Giám thị 2";
        string tenCotNgayCoiThi = "Ngày";
        string tencotCaLamViec = "Ca học";
        string tenCotSTT = "STT"; // STT dùng để tô màu dòng bị trùng
        int sttColor = 0; // Đếm số thứ tự GV, stt GV hiện tại -> lấy màu 

        DataView dataView; // dùng để lọc dữ liệu theo giám thị 1
        DataTable tblData;

        private bool isLoaded = false; // Cờ để theo dõi trạng thái form, liên quan đến SelectedIndexChanged cmbGV 
        string cmbGVTieuDe = "Xem tất cả";


        // Tạo một HashSet lưu các phần tử là stt các hàng bị trùng lịch
        HashSet<double> dsTrungLich = new HashSet<double>();


        string[] MangCotThongTin;
        string[] MangTieuDeCotThongTin;
        int soLuongNV,soCot, soDong;


        private string GetColor(int index)
        {
            // Danh sách màu sắc  
            string[] colors = {
            "LightGreen"
        };
            return colors[index % colors.Length]; // Lặp lại màu sắc nếu vượt quá số màu
        }


        public Form1()
        {
            InitializeComponent();
        }

        public void XoaLuoiDuLieu()
        {
            oDs.Tables.Clear();
            oGrid.DataSource = null;
        }

        private void btDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<string> layDanhSachGv()
        {
            // Bước - lấy danh sách tất cả giảng viên
            var danhSachGiamThi = oDs.Tables[tenTableThongtinGV]
            .AsEnumerable() // Chuyển đổi thành Enumerable
            .Select(row => row.Field<string>(tencotGT1)) // Lấy giá trị từ cột "Giám thị 1"
            .Concat(oDs.Tables[tenTableThongtinGV] // Kết hợp với giá trị từ cột "Giám thị 2"
                .AsEnumerable()
                .Select(row => row.Field<string>(tencotGT2)))
            .Distinct() // Lọc ra các giá trị không trùng
            .ToList(); // Chuyển đổi thành danh sách

            return danhSachGiamThi;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            dsTrungLich.Clear(); // danh sách stt các dòng bị trùng lịch

            // Bước - lấy danh sách tất cả giảng viên
            var danhSachGiamThi = oDs.Tables[tenTableThongtinGV]
            .AsEnumerable() // Chuyển đổi thành Enumerable
            .Select(row => row.Field<string>(tencotGT1)) // Lấy giá trị từ cột "Giám thị 1"
            .Concat(oDs.Tables[tenTableThongtinGV] // Kết hợp với giá trị từ cột "Giám thị 2"
                .AsEnumerable()
                .Select(row => row.Field<string>(tencotGT2)))
            .Distinct() // Lọc ra các giá trị không trùng
            .ToList(); // Chuyển đổi thành danh sách
                       // Bước - kiểm tra
                       //      Với mổi giảng viên trong danh sách 
                       //      Chọn toàn bộ các dòng mà giảng viên là giám thị 1 hay giám thị 2
                       //        báo lổi nếu :  Với mổi ngày trong danh sách của giảng viên đã chọn, có 2 ca trùng nhau  
            foreach (var gt1 in danhSachGiamThi)
            {
                string colorGV = GetColor(sttColor); // lấy màu của GV thứ sttColor

                // Lấy danh sách các hàng thỏa mãn điều kiện và nhóm theo ngày và ca làm việc
                var groupedKiemTra = oDs.Tables[tenTableThongtinGV]
                    .AsEnumerable() // Chuyển đổi thành Enumerable
                    .Select(row => new
                    {
                        Ngay = row.Field<DateTime>(tenCotNgayCoiThi),
                        CaLamViec = row.Field<double>(tencotCaLamViec),
                        GiamThi1 = row.Field<string>(tencotGT1),
                        GiamThi2 = row.Field<string>(tencotGT2),
                        STT = row.Field<double>(tenCotSTT)
                    })
                    .Where(item => item.GiamThi1 == gt1 || item.GiamThi2 == gt1) // Lọc ngay sau khi chọn
                    .GroupBy(item => new
                    {
                        item.Ngay,
                        item.CaLamViec
                    }) // Nhóm theo ngày và ca làm việc
                    .Select(group => new
                    {
                        Ngay = group.Key.Ngay, // Lấy ngày từ khóa của nhóm
                        CaLamViec = group.Key.CaLamViec, // Lấy ca làm việc từ khóa của nhóm
                        GiamsThi = group.Select(g => new
                        {
                            GiamThi1 = g.GiamThi1, // Lấy giá trị giám thị 1 từ từng phần tử trong nhóm
                            GiamThi2 = g.GiamThi2,  // Lấy giá trị giám thị 2 từ từng phần tử trong nhóm
                            STT = g.STT
                        }).ToList() // Chuyển đổi danh sách giám thị thành một danh sách
                    })
                    .ToList(); // Chuyển đổi kết quả cuối cùng thành danh sách
                // Lọc và lấy các nhóm có số lượng dòng chi tiết > 1 -> bị trùng 
                var filteredGroups = groupedKiemTra
                    .Where(group => group.GiamsThi.Count > 1) // Chọn nhóm có số lượng dòng chi tiết > 1
                    .ToList();
                // Lấy STT các dòng bị trùng ngày + ca làm việc -> tô màu nền 
                foreach (var group in filteredGroups)
                    foreach (var giamThi in group.GiamsThi)
                    {
                        dsTrungLich.Add(giamThi.STT);
                        ToMauGrid(giamThi.STT, colorGV); // -> tô màu nền , mổi giám thị 1 màu khác nhau
                    }
                sttColor++; // giảng viên tiếp theo
            }
        }

        private void ToMauGrid(double sttDong,string colorGV)
        {
            foreach (DataGridViewRow row in oGrid.Rows)
            {
                // Kiểm tra xem giá trị trong cột STT có bằng targetSTT không
                if (row.Cells["STT"].Value != null && row.Cells["STT"].Value.ToString() == sttDong.ToString())
                {
                    // Tô màu nền đỏ cho dòng này
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromName(colorGV);
                    // Dừng vòng lặp ngay sau khi đã tô màu cho một dòng
                    break;
                }
            }
        }

      
        private void btExcel_Click(object sender, EventArgs e)
        {
            {
                string ConnectionString;
                string tenFile;

                // /////////////// chon tap tin 
                OpenFileDialog1.Filter = "Tập tin Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                OpenFileDialog1.Title = " Chọn tập tin dữ liệu ";
                OpenFileDialog1.FileName = "";
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFileDuLieu.Text = OpenFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                // ////////////////////////////////////////////////////////
                XoaLuoiDuLieu();
                tenFile = txtFileDuLieu.Text; // tenFile = Application.StartupPath & "\" & "dataExcel.xls"
                // Kiểm tra định dạng file
                if (tenFile.EndsWith(".xls")) // phương thức của lớp string,kiểm tra xem một chuỗi có kết thúc bằng một chuỗi khác hay không
                {
                    ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tenFile + ";Extended Properties='Excel 8.0;HDR=YES';";
                }
                else if (tenFile.EndsWith(".xlsx"))
                {
                    ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + tenFile + ";Extended Properties='Excel 12.0 Xml;HDR=YES';";
                }
                else
                {
                    throw new Exception("Định dạng file không hợp lệ. Chỉ hỗ trợ .xls và .xlsx.");
                }

                using (OleDbConnection oCnn = new OleDbConnection(ConnectionString))
                {
                    try
                    {
                        oCnn.Open();
                        // Lấy tên bảng đầu tiên
                        DataTable dtSchema = oCnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string firstSheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                        // Đọc dữ liệu từ bảng đầu tiên
                        string query = $"SELECT * FROM [{firstSheetName}]";
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, oCnn))
                        {
                            oDs.Clear();
                            adapter.Fill(oDs, tenTableThongtinGV);
                            tblData = oDs.Tables[tenTableThongtinGV];
                            // Sắp xếp dữ liệu theo cột ngày
                            dataView = new DataView(tblData); 
                            dataView.Sort = tenCotNgayCoiThi +  " ASC, " + tencotCaLamViec + " ASC";
                            // Gán DataView làm nguồn dữ liệu cho GridView
                            oGrid.DataSource = dataView;

                            soLuongNV = oGrid.RowCount - 1;  // không đếm dòng tiêu đề
                            txtSoLuongNV.Text = soLuongNV.ToString();
                            soCot = oGrid.Columns.Count; // chu y, cot sẽ bat dau tu 0
                            soDong = oGrid.Rows.Count - 1; // dong tren luoi luôn có 1 dòng empty bên dưới
                                                           //chu y, dong sẽ bat dau tu 0 (trong do cos 1 dong tieu de trong file Excel)
                                                           // có 1 dòng là bắt đầu từ 0 đến 0



                            // Gọi hàm để lấy danh sách giám thị
                            var danhSachGiamThi = layDanhSachGv();
                            // Tạo danh sách mới và thêm "Chọn" vào đầu
                            var danhSachComboBox = new List<string> { cmbGVTieuDe }; // Thêm dòng "Chọn"
                            danhSachComboBox.AddRange(danhSachGiamThi); // Thêm các giá trị từ danh sách giám thị
                            // Gán danh sách vào ComboBox
                            cmbGV.DataSource = danhSachComboBox;

                            // Định dạng hiển thị sau khi đã load dữ liệu cho lưới
                            oGrid.ScrollBars = ScrollBars.Both; // Hoặc ScrollBars.Vertical hoặc ScrollBars.Horizontal
                            oGrid.Columns[tenCotNgayCoiThi].DefaultCellStyle.Format = "dd/MM/yyyy";
                            oGrid.Columns[tenCotSTT].DefaultCellStyle.Format = "N0";
                            oGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }

           

            }
            isLoaded = true; // Đặt cờ là true khi form đã tải xong, ogrid đã có dữ liệu
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím nhấn là Esc
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Đóng form
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dieuChinhKichThuocForm();
        }

        private void cmbGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị đã chọn từ ComboBox
            string giaTriChon = cmbGV.SelectedItem?.ToString();
            // Kiểm tra nếu giá trị là "Chọn"
            if (string.IsNullOrEmpty(giaTriChon) || giaTriChon == cmbGVTieuDe)
            {
                dataView.RowFilter = ""; // Thiết lập RowFilter thành chuỗi rỗng để xem tất cả
                dataView.Sort = tenCotNgayCoiThi + " ASC"; // Sắp xếp dữ liệu theo cột ngày
                oGrid.DataSource = dataView;
                return; // Không thực hiện thêm bất kỳ hành động nào
            }

            // Lọc dữ liệu theo giá trị đã chọn
            if (!string.IsNullOrEmpty(giaTriChon))
            {
                // Thiết lập DataView với bộ lọc
                string dieuKienLoc = "[" + tencotGT1 + "] = '" + giaTriChon+"'";
                dataView.RowFilter = dieuKienLoc; // Thay đổi tenCotGiua thành tên cột cần lọc
                dataView.Sort = tenCotNgayCoiThi + " ASC"; // Sắp xếp dữ liệu theo cột ngày
                oGrid.DataSource = dataView;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void dieuChinhKichThuocForm()
        {
            // Tính toán chiều cao và chiều rộng mới
            int newHeight = this.Height; // Chiều cao bằng chiều cao của form 
            int newWidth = this.Width; // Chiều rộng bằng chiều rộng của form

            // Cập nhật kích thước cho GridView
            oGrid.Height = newHeight-150;
            oGrid.Width = newWidth;

            // Đặt vị trí của DataGridView để đáy nằm sát đáy của form
            oGrid.Top = this.ClientSize.Height - oGrid.Height;  // Đặt vị trí trên cùng
        }

    }
}
