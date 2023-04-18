import 'models/sender.dart';

String senderName(Sender sender) {
  return sender.displayName.isNotEmpty ? sender.displayName : sender.email;
}

String since(DateTime timestamp) {
  final diff = DateTime.now().difference(timestamp);
  if (diff.inDays > 0) {
    return '${diff.inDays} days';
  } else if (diff.inHours > 0) {
    return '${diff.inHours} hours';
  } else if (diff.inMinutes > 0) {
    return '${diff.inMinutes} min';
  } else {
    return 'Now';
  }
}
