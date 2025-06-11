
function printElementById2(elementId) {
    var content = document.getElementById(elementId);
    if (!content) {
        alert("Không tìm thấy phần cần in!");
        return;
    }

    // Tìm canvas trong phần cần in
    var canvas = content.querySelector('canvas#chartBanHang');
    var imgHTML = "";

    if (canvas) {
        // Chuyển canvas thành ảnh base64
        var imgData = canvas.toDataURL("image/png");
        imgHTML = `<img src="${imgData}" style="max-width:100%; height:auto;" />`;
    } else {
        // Nếu không tìm thấy canvas thì lấy nội dung html bình thường
        imgHTML = content.innerHTML;
    }

    var pri = window.open('', '_blank', 'width=800,height=600');
    pri.document.open();
    pri.document.write(`
        <html>
        <head>
            <title>In báo cáo</title>
            <link rel="stylesheet" href="lib/bootstrap/dist/css/bootstrap.min.css" />
            <style>
                body { padding: 10px; }
            </style>
        </head>
        <body>
            ${imgHTML}
        </body>
        </html>
    `);
    pri.document.close();
    pri.focus();

    setTimeout(() => {
        pri.print();
        pri.close();
    }, 500);
}

   async function printElementById3(elementId) { {
    const element = document.getElementById(elementId);
    if (!element) {
        alert("Không tìm thấy phần tử cần in!");
        return;
    }

    // Dùng html2canvas để render phần tử thành canvas
    const canvas = await html2canvas(element, { scale: 2 });
    const dataUrl = canvas.toDataURL("image/png");

    // Tạo cửa sổ mới chứa ảnh và in
    const printWindow = window.open('', '_blank', 'width=900,height=700');
    printWindow.document.write(`
        <html>
            <head><title>In báo cáo</title></head>
            <body style="text-align: center; padding: 20px;">
                <img src="${dataUrl}" style="max-width: 100%; height: auto;" />
            </body>
        </html>
    `);
    printWindow.document.close();
    printWindow.focus();
    printWindow.onload = () => {
        printWindow.print();
        printWindow.close();
    };
} 

    async function printElementById3(elementId) {
        const element = document.getElementById(elementId);
        if (!element) {
            alert("Không tìm thấy phần tử cần in!");
            return;
        }

        const canvas = await html2canvas(element, { scale: 2 });
        const dataUrl = canvas.toDataURL("image/png");

        const printWindow = window.open('', '_blank', 'width=900,height=700');
        printWindow.document.write(`
            <html>
                <head><title>In báo cáo</title></head>
                <body style="text-align: center; padding: 20px;">
                    <img src="${dataUrl}" style="max-width: 100%; height: auto;" />
                </body>
            </html>
        `);
        printWindow.document.close();
        printWindow.focus();
        printWindow.onload = () => {
            printWindow.print();
            printWindow.close();
        };
    }
   } 