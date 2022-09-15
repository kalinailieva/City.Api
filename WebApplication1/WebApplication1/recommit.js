function loadTable(model) {
	// initialize controls
	$('#SiteFrom').select2();
	$('#SiteTo').select2();

	$('#datepicker-from').datepicker({
		dateFormat: 'DD, dd MM yy',
		onClose: function (dateText, inst) {
			$(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
		},
		beforeShowDay: function (date) {
			var today = new Date();
			var nextDay = today;

			nextDay.setDate(today.getDate());

			return [(date < nextDay)];
		}
	});

	$('#datepicker-to').datepicker({
		dateFormat: 'DD, dd MM yy',

		onClose: function (dateText, inst) {
			$(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));

		},
		beforeShowDay: function (date) {
			var today = new Date($("#datepicker-from").val());
			var nextDay = today;
			nextDay.setDate(today.getDate());
			return [(date >= nextDay)];
		}
	});

	$('#datepicker-from').datepicker('setDate', '0');

	$('#datepicker-to').datepicker('setDate', '0');

	$('#SiteFrom').on('change', function (event) {
		var selectedIndex = $('#SiteFrom').prop('selectedIndex');
		$('#SiteTo').val($('#SiteFrom').val());
		$('#SiteTo > option').removeAttr('disabled');
		$('#SiteTo > option:lt(' + selectedIndex + ')').attr('disabled', 'disabled');
		$('#SiteTo').trigger('change');
		$('#SiteTo').select2();
	});

	var tableName = 'dataentries';
	tableMessageBus.$data.filterFields[tableName] = {};
	tableMessageBus.$data.tableRows[tableName] = [];
	tableMessageBus.$data.filterNavigation[tableName] = [];
	tableMessageBus.$data.tableDataResult[tableName] = {};

	// filter field
	LoadFilters();
	tableMessageBus.$data.filterNavigation[tableName]["pageSize"] = model.pageSize;
	tableMessageBus.$data.filterNavigation[tableName]["getAllPages"] = model.getAllPages;
	tableMessageBus.$data.filterNavigation[tableName]["currentPage"] = model.currentPage;

	// Recommit table
	Vue.component('dataentry-table-head', {
		template:
			`<thead>
            <tr>
				<th width="10%">
                    <label class='checkbox d-inline-flex' >
                        <input type="checkbox" v-on:click="checkAll(this,$event)" id="ckb-select-all" >
                        <span class="checkmark"></span>
                    </label>		
				</th >
				<th width="20%" id="recommmit-siteName">${localizer('Site')}</th>
				<th width="60%" id="recommit-date">${localizer('Date')}</th>

				<th width="10%"></th>
            </tr>
            </thead>`,

		methods: {
			checkAll: function (value, event) {

				var isChecked = event.target.checked;
				console.log(event.target.dataEntryId)
				tableMessageBus.$emit('checkAll', isChecked);
				GetSelectedData();
			}
		}
	});

	Vue.component('dataentry-table-body', {
		template:
			`<tbody >
            <tr is="dataentry-table-row"
                v-for="(row, index) in rows"
                v-bind:key="row.dataEntryId"
                v-bind:row="row">
            </tr>
        </tbody>`,

		data: function () {
			return {
				rows: tableMessageBus.$data.tableRows[tableName],
			};
		}
	});

	Vue.component('dataentry-table-row', {
		template:
			`<tr >
				<td>
					<div class="d-flex align-items-center">
						<label class='checkbox d-inline-flex'>
							<input type="checkbox"  :checked="row.isChecked" v-on:click="selectRow($event.target.checked)" >
							<span class="checkmark"></span>
						</label>
					
					</div>
				</td>
				<td> 

				</td>
				<td> {{ row.date }}  #{{row.dataEntryPartNumber}} </td>
				<td >
				</td>
        </tr>`,

		props: ['row'],

		methods: {
			selectRow: function (isChecked) {
				this.row.isChecked = isChecked;
			},
		},
		created: function () {
			tableMessageBus.$on('checkAll', this.selectRow);

		},
	});

	// initilize app
	var rawDocumentTable = new Vue({
		el: '#app',

	});

	// search
	$("#search-button").on("click", function () {

		// filter field
		LoadFilters();
		console.log('clicked');

		tableMessageBus.$data.filterNavigation[tableName]["currentPage"] = 1;
		tableMessageBus.$emit('filter-items');
		/*tableMessageBus.$emit('data-reloaded'); doesn't work as expected - to clear the table result'*/
	});

	function LoadFilters() {

		tableMessageBus.$data.filterFields[tableName]["SiteFrom"] = $('#SiteFrom').val();
		tableMessageBus.$data.filterFields[tableName]["SiteTo"] = $('#SiteTo').val();
		tableMessageBus.$data.filterFields[tableName]["DateFrom"] = $('#datepicker-from').val();
		tableMessageBus.$data.filterFields[tableName]["DateTo"] = $('#datepicker-to').val();
	}



	function GetSelectedData() {

		var selected = [];
		tableMessageBus.$data.tableRows[tableName]
			.forEach(function (item, ind, arr) {
				if (item.isChecked && jQuery.inArray(item.dataEntryId, tableMessageBus.$data.sendIds) < 0) {
					selected.push(item.dataEntryId);
				}
			});
		console.log(selected.values);
		return selected;
	}
}


