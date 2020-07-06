extends TouchScreenButton

var radius = Vector2(20,20)
var boundary = 100
var ongoing_draw = -1
var return_accel = 20
var threshold = 10

signal get_value

func _process(delta):
	if ongoing_draw == -1:
		var pos_difference = ( Vector2(0,0) - radius * global_scale ) - position
		position += pos_difference * return_accel * delta
	emit_signal("get_value", get_value())


func get_button_pos():
	return position + radius * global_scale

func _input(event):
	if event is InputEventScreenDrag or ( event is InputEventScreenTouch and event.is_pressed() ):

		var event_distance_from_center = ( event.position - get_parent().global_position).length()
		
		if event_distance_from_center <= boundary * global_scale.x or event.get_index() == ongoing_draw:
			global_position = event.position - radius * global_scale

			if get_button_pos().length() > boundary:
				position = get_button_pos().normalized() * boundary - radius
			
		ongoing_draw = event.get_index()

	if event is InputEventScreenTouch and !event.is_pressed() and event.get_index()==ongoing_draw:
		ongoing_draw = -1
		
func get_value():
	if get_button_pos().length() > threshold:
		return get_button_pos().normalized()
	return Vector2(0,0)

