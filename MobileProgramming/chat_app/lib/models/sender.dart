class SenderKeys {
  static const uid = 'uid';
  static const displayName = 'displayName';
  static const email = 'email';
}

class Sender {
  final String uid;
  final String displayName;
  final String email;

  Sender({required this.uid, required this.displayName, required this.email});

  Sender.fromMap(Map<String, dynamic> data)
      : uid = data[SenderKeys.uid],
        displayName = data[SenderKeys.displayName],
        email = data[SenderKeys.email];

  toMap() {
    return {
      SenderKeys.uid: uid,
      SenderKeys.displayName: displayName,
      SenderKeys.email: email
    };
  }
}
