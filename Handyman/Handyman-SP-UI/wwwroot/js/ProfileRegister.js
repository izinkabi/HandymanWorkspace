
// profile container
const container = document.querySelector(".profile-container");

// swap create-profile panel with add-address panel -> (Forward Direction)
export function swapToCreateProfile() {
    container.classList.remove("create-profile-mode");

};

// swap create-profile panel with add-address panel -> ( Reverse Direction )
export function swapToAddAddress() {
    
    container.classList.add("create-profile-mode");
  
};
