export function ReloadPage() {
    /*reload the page*/
    location.reload();
}

export function ReloadPageThreeSecDelay() {
    /*Add 3 seconds delay */
    setTimeout(() => {
        document.location.reload();
    }, 10000);
}