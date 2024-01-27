
export function openQRCode() {
    window.addEventListener("load", () => {
        const uri = document.getElementById("qrCodeData").getAttribute('data-url');
        new QRCode(document.getElementById("qrCode"),
            {
                text: uri,
                width: 150,
                height: 150
            });
    });
};

export function clearQRCode() {
    qrcode.clear(); // clear the code.
    qrcode.makeCode("http://naver.com"); // make another code.
}

