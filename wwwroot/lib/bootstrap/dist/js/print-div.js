   function printDivById(divId) {
        // Trích xuất nội dung canvas và chuyển thành ảnh
        const revenueCanvas = document.getElementById("revenueChart");
        const revenueImg = document.getElementById("revenueChartImg");
        revenueImg.src = revenueCanvas.toDataURL("image/png");

        const orderCanvas = document.getElementById("orderCountChart");
        const orderImg = document.getElementById("orderCountChartImg");
        orderImg.src = orderCanvas.toDataURL("image/png");

        // Hiện ảnh, ẩn canvas
        revenueCanvas.style.display = "none";
        revenueImg.style.display = "block";
        orderCanvas.style.display = "none";
        orderImg.style.display = "block";

        // Lấy nội dung cần in
        const content = document.getElementById(divId).innerHTML;
        const printWindow = window.open('', '', 'width=1000,height=700');
        printWindow.document.write(`
            <html>
            <head>
                <title>In Báo Cáo</title>
                <link rel="stylesheet" href="lib/bootstrap/dist/css/bootstrap.min.css">
                <style>
                    body { font-family: Arial, sans-serif; padding: 20px; }
                    h3 { text-align: center; }
                    img { max-width: 100%; height: auto; }
                </style>
            </head>
            <body onload="window.print(); window.close();">
                ${content}
            </body>
            </html>
        `);
        printWindow.document.close();

        // Sau khi in xong, khôi phục lại trạng thái hiển thị
        setTimeout(() => {
            revenueCanvas.style.display = "block";
            revenueImg.style.display = "none";
            orderCanvas.style.display = "block";
            orderImg.style.display = "none";
        }, 1000);
    }