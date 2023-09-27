
const container = document.querySelector(".container");

export function swapCreateProfile() {

    container.classList.remove("sign-up-mode");

};

export function swapCreateAddress() {

    container.classList.add("sign-up-mode");
  
};
