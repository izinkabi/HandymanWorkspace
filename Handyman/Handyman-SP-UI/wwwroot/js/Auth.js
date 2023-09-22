const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".authContainer");

export function swapLogin() {
   
    //sign_in_btn.addEventListener("click", () => {
        container.classList.remove("sign-up-mode");
   // });
    
};

export function swapRegister() {
   
    //sign_up_btn.addEventListener("click", () => {
        container.classList.add("sign-up-mode");
    //});
};
