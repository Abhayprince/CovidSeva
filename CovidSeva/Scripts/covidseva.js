function copyToClipboard(text) {
    var textToClipboard = text;

    var success = true;
    if (window.clipboardData) { // Internet Explorer
        window.clipboardData.setData("Text", textToClipboard);
    }
    else {
        // create a temporary element for the execCommand method
        var forExecElement = CreateElementForExecCommand(textToClipboard);

        /* Select the contents of the element 
            (the execCommand for 'copy' method works on the selection) */
        SelectContent(forExecElement);

        var supported = true;

        // UniversalXPConnect privilege is required for clipboard access in Firefox
        try {
            if (window.netscape && netscape.security) {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }

            // Copy the selected content to the clipboard
            // Works in Firefox and in Safari before version 5
            success = document.execCommand("copy", false, null);
        }
        catch (e) {
            success = false;
        }

        // remove the temporary element
        document.body.removeChild(forExecElement);
    }

    if (success) {
        //alert("The text is on the clipboard, try to paste it!");
        alert('Copied to Clipboard');
    }
    else {
        //alert("Your browser doesn't allow clipboard access!");
        prompt("Unable to Access Clipboard - Your browser doesn't allow clipboard access!<br /> To Copy to clipboard: Ctrl+C, Enter", text);
    }
}

function CreateElementForExecCommand(textToClipboard) {
    var forExecElement = document.createElement("div");
    // place outside the visible area
    forExecElement.style.position = "absolute";
    forExecElement.style.left = "-10000px";
    forExecElement.style.top = "-10000px";
    // write the necessary text into the element and append to the document
    forExecElement.textContent = textToClipboard;
    document.body.appendChild(forExecElement);
    // the contentEditable mode is necessary for the  execCommand method in Firefox
    forExecElement.contentEditable = true;

    return forExecElement;
}

function SelectContent(element) {
    // first create a range
    var rangeToSelect = document.createRange();
    rangeToSelect.selectNodeContents(element);

    // select the contents
    var selection = window.getSelection();
    selection.removeAllRanges();
    selection.addRange(rangeToSelect);
}

$(function () {

    var cities = {}; //{stateId:[{cityId: 1, cityName: 'Noida'}]};

    var Urls = {
        cities: '/ajax/cities',
        Add: '/ajax/add',
        Search: '/ajax/search'
    }

    $(document).on('change', '[name=search-service-type]:radio', function (e) {
        var btn = $(this);
        $('.type-radio').removeClass('selected');
        if (btn.prop('checked')) {
            btn.closest('.type-radio').addClass('selected');
        }
    });

    $(document).on('click', '[data-qshare-btn]', function (e) {
        e.preventDefault();
        var btn = $(this);
        var shareType = btn.data('qshare-type');
        var shareContent = btn.data('qshare-content');
        var shareURL = 'http://covid-seva.appdror.com';

        if (shareType == 'copy') {
            copyToClipboard(shareContent + ' - ' + shareURL);
        }
        else if (shareType == 'fb') {
            var url = 'https://www.facebook.com/sharer/sharer.php?sdk=joey';
            url += '&u=' + shareURL;
            url += '&display=popup&ref=plugin';

            var popupWindow = window.open(url, 'FB Share', 'height=200,width=300');
            popupWindow.focus();
        }
        else if (shareType == 'tw') {
            var url = 'https://twitter.com/intent/tweet?original_referer=' + location.href;
            url += '&text=' + shareContent;
            url += '&tw_p=tweetbutton&url=' + shareURL;

            var popupWindow = window.open(url, 'Twitter Share', 'height=270,width=400');
            popupWindow.focus();
        }

        else if (shareType == 'li') {
            var popupWindow = window.open('https://www.linkedin.com/shareArticle?mini=true&url=' + shareURL + '&title=' + encodeURIComponent(shareContent) + '&summary=' + encodeURIComponent(shareContent) + '&source=LinkedIn', '', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600');
            popupWindow.focus();
        }
    });
    $(document).on('click', '.copy-number', function (e) {
        var number = $(this).data('number');
        copyToClipboard(number);
    })

    function loadCities(stateId, cityDdl) {
        if (cities[stateId]) {
            bindCities(cities[stateId], cityDdl);
        }
        else {
            $.ajax({
                url: Urls.cities,
                data: {
                    stateId
                },
                success: function (data) {
                    if (data && data.length) {
                        cities[stateId] = data;
                        bindCities(data, cityDdl);
                    }
                },
                error: function (a, b, c) {
                    console.error(a, b, c);
                }
            })

        }
    }
    function bindCities(citiesList, cityDdl) {
        cityDdl.empty();
        var options = '<option value="0">Select City</option>';
        options += citiesList.map((c) => '<option value="' + c.Id + '">' + c.Name + '</option>').join();
        cityDdl.append(options);
    }

    $(document).on('change', '#add-state', function (e) {
        var stateId = $(this).val();
        var cityDdl = $('#add-city')
        if (stateId) {
            loadCities($(this).val(), cityDdl);
        }
        else {
            cityDdl.empty();
            var option = '<option value="0">Select City</option>';
            cityDdl.append(option);
        }
    });
    $(document).on('change', '#search-state', function (e) {
        var stateId = $(this).val();
        var cityDdl = $('#search-city')
        if (stateId) {
            loadCities($(this).val(), cityDdl);
        }
        else {
            cityDdl.empty();
            var option = '<option value="0">Select City</option>';
            cityDdl.append(option);
        }
    });

    function resetAddForm() {
        $('[name=add-service-type]').prop('checked', false);
        $('#add-contact,#add-name,#add-state,#add-city,#add-address,#add-remark').val('');
    }
    var saving = false;
    $(document).on('click', '#add-btn', function (e) {
        e.preventDefault();
        if (!saving) {
            saving = true;
                var msgs = [];
                var serviceType = $('[name=add-service-type]:checked').val();
                var contact = $('#add-contact').val();
                if (!serviceType) {
                    msgs.push('Please select the service/resource type');
                }
                if (!contact || !contact.trim() || contact.length != 10) {
                    msgs.push('Please enter a valid 10-digit contact number');
                }
                if (msgs.length > 0) {
                    alert(msgs.join('\n'));
                    saving = false;
                    return false;
                }
                else {
                    var name = $('#add-name').val();
                    if (name) {
                        name = name.replace(/\s{2,}/g, ' ').trim();
                    }
                    var stateId = $('#add-state').val() || 0;
                    var cityId = $('#add-city').val() || 0;
                    var address = $('#add-address').val();
                    var remarks = $('#add-remark').val();
                    if (address) {
                        address = address.replace(/\s{2,}/g, ' ').trim();
                    }
                    if (remarks) {
                        remarks = remarks.replace(/\s{2,}/g, ' ').trim();
                    }

                    var data = { serviceType, contact, name, address, stateId, cityId, remarks };
                    $.ajax({
                        url: Urls.Add,
                        data: data,
                        type: 'POST',
                        success: function (d) {
                            if (d && d.length > 0) {
                                var errors = d.join('\n');
                                alert(errors);
                                saving = false;
                                return false;
                            }
                            saving = false;
                            resetAddForm();
                            alert('THANK YOU.\nInformation saved successfully.');
                            $('#add-info-modal').modal('hide');
                        },
                        error: function (a, b, c) {
                            alert('Oops something went wrong');
                            console.error(a, b, c);
                            saving = false;
                        }
                    })                
            }
        }
    });

    var resultsContainer = $('#results-container');

    $(document).on('click', '#search-btn', function (e) {
        var serviceType = $('[name=search-service-type]:checked').val();
        var name = $('#search-name').val();
        if (name) {
            name = name.replace(/\s{2,}/g, ' ').trim();
        }
        var stateId = $('#search-state').val() || 0;
        var cityId = $('#search-city').val() || 0;
        var freetext = $('#search-freetext').val();        
        if (freetext) {
            freetext = freetext.replace(/\s{2,}/g, ' ').trim();
        }

        var data = { serviceType, name, stateId, cityId, freetext };
        $.ajax({
            url: Urls.Search,
            data: data,
            type: 'POST',
            accepts: 'text/html',
            success: function (html) {
                resultsContainer.html(html);
            },
            error: function (a, b, c) {
                alert('Oops something went wrong');
                console.error(a, b, c);
            }
        })     
    });
})