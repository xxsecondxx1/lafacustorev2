from flapython app.pysk import Flask, request, jsonify # type: ignore
from database import add_email

app = Flask(__name__)

@app.route('/subscribe', methods=['POST'])
def subscribe():
    data = request.get_json()
    email = data.get('email')

    if not email or '@gmail.com' not in email:
        return jsonify({'error': 'Invalid email address'}), 400

    success = add_email(email)

    if success:
        return jsonify({'message': 'Email subscribed successfully'}), 200
    else:
        return jsonify({'error': 'Email already subscribed'}), 400

if __name__ == '__main__':
    app.run(debug=True)