let revenueChartInstance = null; // Khai báo biến toàn cục để lưu instance biểu đồ
let orderChartInstance = null; // Khai báo biến toàn cục để lưu instance biểu đồ

window.renderCharts = (doanhThuData, donHangData) => {
    // Ẩn ảnh và hiển thị lại canvas (nếu cần)
    document.getElementById("revenueChartImg").style.display = "none";
    document.getElementById("revenueChart").style.display = "block";
    document.getElementById("orderCountChartImg").style.display = "none";
    document.getElementById("orderCountChart").style.display = "block";

    // Hủy biểu đồ doanh thu cũ nếu nó tồn tại
    if (revenueChartInstance) {
        revenueChartInstance.destroy();
    }

    const ctx1 = document.getElementById("revenueChart").getContext("2d");
    revenueChartInstance = new Chart(ctx1, { // Gán instance mới vào biến
        type: 'bar',
        data: {
            labels: Object.keys(doanhThuData),
            datasets: [{
                label: 'Doanh thu',
                data: Object.values(doanhThuData),
                backgroundColor: 'rgba(75, 192, 192, 0.6)'
            }]
        }
    });

    // Hủy biểu đồ đơn hàng cũ nếu nó tồn tại
    if (orderChartInstance) {
        orderChartInstance.destroy();
    }

    const ctx2 = document.getElementById("orderCountChart").getContext("2d");
    orderChartInstance = new Chart(ctx2, { // Gán instance mới vào biến
        type: 'bar',
        data: {
            labels: Object.keys(donHangData),
            datasets: [{
                label: 'Số lượng đơn',
                data: Object.values(donHangData),
                backgroundColor: 'rgba(255, 159, 64, 0.6)'
            }]
        }
    });

    // Chuyển canvas -> ảnh để in (nếu bạn vẫn muốn chức năng này)
    setTimeout(() => {
        const img1 = document.getElementById("revenueChartImg");
        img1.src = document.getElementById("revenueChart").toDataURL("image/png");
        img1.style.display = "block";
        document.getElementById("revenueChart").style.display = "none";

        const img2 = document.getElementById("orderCountChartImg");
        img2.src = document.getElementById("orderCountChart").toDataURL("image/png");
        img2.style.display = "block";
        document.getElementById("orderCountChart").style.display = "none";
    }, 1000);
};

    window.veBieuDoCot = function (labels, data) {
        const canvas = document.getElementById('chartBanHang');
        if (!canvas) {
            console.error('Không tìm thấy thẻ canvas với id "chartBanHang"');
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error('Không thể lấy context 2D từ canvas');
            return;
        }

        // Xoá biểu đồ cũ nếu có
        if (window.myChart) {
            window.myChart.destroy();
            console.log('Đã xoá biểu đồ cũ');
        }

        // Vẽ biểu đồ mới
        window.myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Số lượng đã bán',
                    data: data,
                    backgroundColor: 'rgba(75, 192, 192, 0.6)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Số lượng'
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Tên sản phẩm'
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Biểu đồ số lượng sản phẩm đã bán'
                    }
                }
            }
        });

        console.log('Đã vẽ lại biểu đồ với dữ liệu mới:', labels, data);
}
