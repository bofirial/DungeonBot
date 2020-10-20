export { }

declare const bootstrap: any;
declare global {
    interface Window {
        launchModal: any;
    }
}

window.launchModal = (modalId) => {
    const modal = new bootstrap.Modal(document.getElementById(modalId));

    modal.show();
};