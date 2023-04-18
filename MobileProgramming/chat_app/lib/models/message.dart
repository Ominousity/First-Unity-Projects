import 'sender.dart';

class MessageKeys {
  static const timestamp = 'timestamp';
  static const from = 'from';
  static const content = 'content';
}

class Message {
  final String id;
  final DateTime? timestamp;
  final Sender from;
  final String content;

  Message(
      {required this.id,
      required this.timestamp,
      required this.from,
      required this.content});

  Message.fromMap(this.id, Map<String, dynamic> data)
      // TODO
      : timestamp = data[MessageKeys.timestamp],
        from = Sender.fromMap(data[MessageKeys.from]),
        content = data[MessageKeys.content];

  Map<String, dynamic> toMap() {
    return {
      MessageKeys.timestamp: timestamp,
      MessageKeys.from: from,
      MessageKeys.content: content
    };
  }
}
