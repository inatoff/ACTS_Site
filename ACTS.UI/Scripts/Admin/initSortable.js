['Disciplines', 'ScienceInterests', 'Projects', 'Publications'].forEach(function (orderableListName) {
    var orderableList = Sortable.create(document.getElementById(orderableListName.toLowerCase()), {
        animation: 150,
        filter: '.js-remove',
        onFilter: function (evt) {
            var el = orderableList.closest(evt.item);
            if (el) {
                var rootEl = orderableList.el,
                    children = rootEl.children,
                    values = Array.prototype
                                  .slice
                                  .call(rootEl.querySelectorAll('input[type=text]'))
                                  .map(function (input) { return input.value });

                for (var i = evt.oldIndex, N = children.length - 1; i < N; i++) {
                    children[i].querySelector('input[type=text]').value = values[i+1];
                }
                rootEl.removeChild(rootEl.lastElementChild)
            }
        },
        onEnd: function (evt) {
            var oldIndex = evt.oldIndex,
                newIndex = evt.newIndex;

            if (oldIndex === newIndex || newIndex == undefined) {
                return;
            }

            var rootEl = orderableList.el,
                children = rootEl.children,
                oldPlace = children[oldIndex],
                newPlace = children[newIndex],
                values = Array.prototype
                                .slice
                                .call(rootEl.querySelectorAll('input[type=text]'))
                                .map(function (input) { return input.value });


            if (oldIndex > newIndex) {
                rootEl.removeChild(newPlace);
                rootEl.insertBefore(newPlace, oldPlace.nextSibling);
            } else {
                rootEl.removeChild(newPlace);
                rootEl.insertBefore(newPlace, oldPlace);
            }

            for (var i = 0; i < children.length; i++) {
                children[i].querySelector('input[type=text]').value = values[i];
            }
        }
    });

    document.getElementById("addBtn" + orderableListName).onclick = function () {
        var targetEl = orderableList.el;

        var el = document.createElement('div');
        el.setAttribute("class", "orderable-item")
        var html =
            ('<input id="{listName}_{count}__Order" name="{listName}[{count}].Order" type="hidden" value="{count}">'
            + '<div class="input-group">'
            + '<input class="form-control text-box single-line" data-val="true" data-val-required="{requiredFieldErrMsg}" id="{listName}_{count}__Value" name="{listName}[{count}].Value" type="text" value="" aria-required="true" aria-invalid="false">'
            + '<span class="input-group-addon js-remove"><i class="fa fa-remove"></i></span>'
            + '</div>'
            + '<p class="text-red field-validation-valid" data-valmsg-for="{listName}[{count}].Value" data-valmsg-replace="true"></p>')
            .replace(/{count}/g, targetEl.children.length)
            .replace(/{listName}/g, orderableListName)
            .replace(/{requiredFieldErrMsg}/g, requiredRes.RequiredFieldErrMsg.replace("{0}", displayRes[orderableListName.substring(0,orderableListName.length - 1) + "Name"]));
        el.innerHTML = html;

        targetEl.appendChild(el);
        el.focus();

        // Updating unobtrusive validation for dynamically generated input
        $("form").data("unobtrusiveValidation", null);
        $("form").data("validator", null);
        $.validator.unobtrusive.parse($("form"));
    }
});