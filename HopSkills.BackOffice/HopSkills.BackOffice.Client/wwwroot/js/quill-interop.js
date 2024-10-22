window.initQuill = function (dotnetHelper) {
    if (!window.Quill) {
        console.error('Quill is not loaded. Make sure to include Quill scripts and styles.');
        return;
    }

    var toolbarOptions = {
        container: [
            ['bold', 'italic', 'underline', 'strike'],
            ['blockquote', 'code-block'],
            [{ 'header': 1 }, { 'header': 2 }],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'script': 'sub' }, { 'script': 'super' }],
            [{ 'indent': '-1' }, { 'indent': '+1' }],
            [{ 'color': [] }, { 'background': [] }],
            ['clean']
        ],
    };

    var quill = new Quill('#quillEditor', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions.container
        }
    });

    if (window.quillContent) {
        quill.root.innerHTML = window.quillContent;
    }

    quill.on('text-change', function () {
        window.quillContent = quill.root.innerHTML;
        dotnetHelper.invokeMethodAsync('UpdateDescription', quill.root.innerHTML);
    });

    window.getQuillContent = function () {
        return quill.root.innerHTML;
    };

    window.quillInstance = quill;
};