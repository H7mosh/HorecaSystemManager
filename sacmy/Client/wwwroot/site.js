window.setRtlClass = (isRtl) => {
    if (isRtl) {
        document.body.classList.add('rtl');
        document.body.classList.remove('ltr');
    } else {
        document.body.classList.add('ltr');
        document.body.classList.remove('rtl');
    }
    // Explicitly set sidebar position
    const sidebar = document.getElementById('sidebar');
    if (sidebar) {
        if (isRtl) {
            sidebar.style.left = 'auto';
            sidebar.style.right = '0';
        } else {
            sidebar.style.left = '0';
            sidebar.style.right = 'auto';
        }
    }
};

window.toggleSidebar = () => {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
};
